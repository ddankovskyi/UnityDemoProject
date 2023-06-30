using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularWandSpellIterator : AWandSpellsIterator
{
    bool _rechargeRequired = false;
    int _spellsQuePointer = 0;

    public RegularWandSpellIterator(List<SpellItem> spellItems) : base(spellItems){}

    public override bool RechargeRequired => _rechargeRequired;

    public override SpellItem GetNext()
    {
        SpellItem nextSpellItem = null;
        while (nextSpellItem == null)
        {
            if (_spellsQuePointer >= _spellItems.Count)
            {
                _rechargeRequired = true;
                return null;
            }
            nextSpellItem = _spellItems[_spellsQuePointer++];
        }
        _rechargeRequired = !CheckIfHasNext();
        return nextSpellItem;
    }

    bool CheckIfHasNext()
    {
        int checkPointer = _spellsQuePointer;
        while (checkPointer < _spellItems.Count) {
            if (_spellItems[checkPointer] != null)
            {
                _spellsQuePointer = checkPointer;
                return true;
            }
            checkPointer++;
        }
        return false;
    }

    public override void Recharge()
    {
        _spellsQuePointer = 0;
    }
}
