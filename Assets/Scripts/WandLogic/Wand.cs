using System;
using UnityEditor.UIElements;
using UnityEngine;
using Zenject;

public class Wand : MonoBehaviour
{

    Transform _firePoint;


    WandItem _wandData;
    float _currentMana;
    public float CurrentMana => _currentMana;
    public float MaxMana => _wandData.Manapool;
    DateTime _readyToShootTime;

    bool IsReadyToShoot => _readyToShootTime < DateTime.Now;


    AWandSpellsIterator _spellsIterator;

    [Inject]
    private SpellsManager _spellManager;

    private void Update()
    {
        RegenMana();
    }

    public void ResetState()
    {
        _spellsIterator.Recharge();
    }

    public void Init(WandItem wandItem, Transform firePoint)
    {
        _firePoint = firePoint;
        _wandData = wandItem;
        _currentMana = _wandData.Manapool;
        _readyToShootTime = DateTime.Now.AddSeconds(-1);
        if (!_wandData.Shuffle)
        {
            _spellsIterator = new RegularWandSpellIterator(_wandData.Spells);
        }
        else
        {
            throw new System.NotImplementedException();
        }

    }

    public void Shoot()
    {
        if (!IsReadyToShoot)
            return;

        var load = PrepareLoad(_wandData.SpellsPerShoot);

        load.Release(_firePoint.position, _firePoint.rotation);
        if (_spellsIterator.RechargeRequired)
        {
            _spellsIterator.Recharge();
            Recharge(load.RechargeTime, load.CastDeley);
        }
        else
        {
            CoolDown(load.CastDeley);
        }
    }

    WandLoad PrepareLoad(int spellsToCast)
    {
        WandLoad load = new WandLoad();
        load.Spread = _wandData.Spread;
        while (spellsToCast > 0)
        {
            SpellItem spellItem = _spellsIterator.GetNext();
            if (spellItem == null)
            {
                return load;
            }
            Spell spell = _spellManager.GetSpellById(spellItem.Id);

            if (spell.ManaCost > _currentMana)
                continue;

            spellsToCast--;
            load.CastDeley += spell.CastDelay;
            load.RechargeTime += spell.RechargeTime;
            load.Spread += spell.Spread;
            _currentMana -= spell.ManaCost;

            if (spell is MulticastSpell multicast)
            {
                spellsToCast += multicast.SpellsQuantity;
                if (multicast.IsUsingFormation && !load.IsUsingFormation)
                {
                    load.IsUsingFormation = true;
                    load.FormationAngle = multicast.FormationAngle;
                }
            }
            else if (spell is ModifierSpell modifier)
            {
                load.Modifyers.Add(modifier);
                spellsToCast++;
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

    void Recharge(float loadRechargeTime, float loadCastDelay)
    {
        var delay = Mathf.Max(_wandData.RechargeTime + loadRechargeTime, _wandData.CastDelay + loadCastDelay);
        _readyToShootTime = DateTime.Now.AddSeconds(delay);
    }

    void CoolDown(float loadCastDelay)
    {
        _readyToShootTime = DateTime.Now.AddSeconds(_wandData.CastDelay + loadCastDelay);
    }


    void RegenMana()
    {
        if (_currentMana < _wandData.Manapool)
        {
            _currentMana += _wandData.ManaChargeSpeed * Time.deltaTime;
            _currentMana = Mathf.Min(_currentMana, _wandData.Manapool);
        }
    }

}
