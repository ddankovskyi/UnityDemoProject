using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class Wand : AItemObject
{

    [SerializeField] Transform _firePoint;

    bool _readyToShoot = true;

    WandItem _state;
    float _currentMana;


    AWandSpellsIterator _spellsIterator;
    private void Update()
    {
        RegenMana();
    }

    public bool WandSpellsChanged { get; set; }

    public override void Init(InventoryItem item)
    {
        if (item is WandItem wandItem)
        {
            _state = wandItem;
            if (!_state.Shuffle)
            {
                _spellsIterator = new RegularWandSpellIterator(_state.Spells);
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }
        else
        {
            Debug.LogError("Wand inited with not wand item data");
        }
    }

    public void UpdateSpellAtPossition(int slotId, SpellItem spell)
    {
        _state.Spells[slotId] = spell;
    }


    public void Shoot()
    {
        if (!_readyToShoot)
            return;

        var load = PrepareLoad(_state.SpellsPerShoot);

        ReleaseLoad(load);
        if (_spellsIterator.RechargeRequired)
        {
            _spellsIterator.Recharge();
            print("Recharge");
            StartCoolDown(_state.RechargeTime + load.RechargeTime);
        }
        else
        {
            StartCoolDown(_state.CastDelay + load.CastDeley);
        }
    }

    void ReleaseLoad(WandLoad load)
    {
        if (load.Spells.Count == 0)
        {
            Debug.Log("Empty load");
            return;
        }

        SpellGameObject previousProjectile = null; // required for arcs
        float formationAngleStep = 0;
        float nextSpellAngle = 0;
        if (load.IsUsingFormation)
        {
            formationAngleStep = load.FormationAngle / load.Spells.Count;
            nextSpellAngle = -load.FormationAngle / 2;
        }


        foreach (SpawnableSpell spell in load.Spells)
        {
            var prefab = Game.Get<SpellsManager>().GetSpellById(spell.Id).Prefab;
            Transform projectileTransform = _firePoint;
            if (load.IsUsingFormation)
            {
                projectileTransform.Rotate(new Vector3(0, 0, nextSpellAngle));
                nextSpellAngle += formationAngleStep;
            }
            else
            {
                var randomAngle = Random.Range(-load.Spread, load.Spread);
                projectileTransform.Rotate(new Vector3(0, 0, randomAngle));
            }

            var spellProjectile = Instantiate(prefab, projectileTransform.position, projectileTransform.rotation);
            foreach (var modifier in load.Modifyers)
            {
                modifier.Apply(spellProjectile, previousProjectile);
            }

            previousProjectile = spellProjectile;
        }
    }

    WandLoad PrepareLoad(int spellsToCast)
    {
        WandLoad load = new WandLoad();

        while (spellsToCast > 0)
        {

            var spell = _spellsIterator.GetNext();
            if (spell == null)
            {
                return load;
            }

            if (spell.ManaCost > _currentMana)
                continue;

            spellsToCast--;
            load.CastDeley += spell.CastDelay;
            load.RechargeTime += spell.RechargeTime;
            load.Spread += spell.Spread;
            _currentMana -= spell.ManaCost;

            if (spell is MulticastSpell multucast)
            {
                spellsToCast += multucast.SpellsQuantity;
                if (multucast.IsUsingFormation && !load.IsUsingFormation)
                {
                    load.IsUsingFormation = true;
                    load.FormationAngle = multucast.FormationAngle;
                }
            }
            else if (spell is ModifierSpell modifier)
            {
                load.Modifyers.Add(modifier);
            }
            else if (spell is SpawnableSpell projectile)
            {
                load.Spells.Add(projectile);
                if (projectile is IWithTrigger withTrigger)
                {
                    var triggerLoad = PrepareLoad(1);
                    withTrigger.Load = triggerLoad;
                    load.RechargeTime += triggerLoad.RechargeTime;
                }
            }
        }

        return load;
    }


    void StartCoolDown(float delaySec)
    {
        if (delaySec > 0)
        {
            StartCoroutine(StartCoolDownCoroutine(delaySec));
        }


        IEnumerator StartCoolDownCoroutine(float delaySec)
        {
            _readyToShoot = false;
            yield return new WaitForSeconds(delaySec);
            _readyToShoot = true;
        }
    }



    void RegenMana()
    {
        if (_currentMana < _state.Manapool)
        {
            _currentMana += _state.ManaChargeSpeed * Time.deltaTime;
            _currentMana = Mathf.Max(_currentMana, _state.Manapool);
        }
    }

}
