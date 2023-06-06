
using System;
using System.ComponentModel;
using UnityEngine;

public abstract class ModifierSpell : Spell
{
    public abstract void Apply(ProjectileSpellGO projectileSpell, ProjectileSpellGO previousSpell);

}

