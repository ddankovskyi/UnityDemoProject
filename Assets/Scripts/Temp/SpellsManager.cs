using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellsManager
{
    Dictionary<string, Spell> _spells;

    public Spell GetSpellById(string spellId)
    {
        _spells.TryGetValue(spellId, out Spell spell);
        return spell;
    }

    public T GetSpellById<T>(string spellId) where T : Spell
    {
        T requiredSpell = GetSpellById(spellId) as T;
        return requiredSpell;
    }

    public SpellsManager()
    {
        _spells = new Dictionary<string, Spell>();
        Resources.LoadAll<Spell>("ScriptableObjects/Spells").ToList().ForEach(
            spell => _spells.Add(spell.Id, spell));
    }

   
}
