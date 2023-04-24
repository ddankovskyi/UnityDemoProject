using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellsManager : IGlobalManager
{
    Dictionary<string, Spell> _spells;

    public Spell GetSpellById(string spellId)
    {
        _spells.TryGetValue(spellId, out Spell spell);
        return spell;
    }

    public void Init()
    {
        _spells = new Dictionary<string, Spell>();
        Resources.LoadAll<Spell>("ScriptableObjects/Spells").ToList().ForEach(
            spell => _spells.Add(spell.Id, spell));
    }

   
}