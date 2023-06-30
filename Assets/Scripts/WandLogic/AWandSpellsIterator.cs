using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWandSpellsIterator
{

    public AWandSpellsIterator(List<SpellItem> spellItems)
    {
        _spellItems = spellItems;
    }

    protected List<SpellItem> _spellItems;
    public abstract SpellItem GetNext();

    public abstract bool RechargeRequired { get; }

    public abstract void Recharge();

}
