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
    public float CurrentMana => _currentMana;

    AWandSpellsIterator _spellsIterator;
    private void Update()
    {
        RegenMana();
    }

    public void ResetState()
    {
        _spellsIterator.Recharge();
    }

    public override void Init(InventoryItem item)
    {
        if (item is WandItem wandItem)
        {
            _state = wandItem;
            _currentMana = _state.Manapool;
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



    public void Shoot()
    {
        if (!_readyToShoot)
            return;

        var load = PrepareLoad(_state.SpellsPerShoot);

        load.Release(_firePoint.position, _firePoint.rotation);
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
            else if (spell is ProjectileSpell projectile)
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
            _currentMana = Mathf.Min(_currentMana, _state.Manapool);
        }
    }

}
