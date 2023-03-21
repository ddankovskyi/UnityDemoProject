using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpellsPool 
{
    [SerializeField] List<Spell> spells;
    int pointer;

    public Spell NextSpell(ref bool shouldRecharge)
    {
        shouldRecharge = false;
        if (spells.Count == 0) return null;
        var res = spells[pointer];
        pointer++;
        if (pointer >= spells.Count)
        {
            pointer = 0;
            shouldRecharge = true;
        }
        return res;
    }
}
