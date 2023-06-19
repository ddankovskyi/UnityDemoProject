using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSpell", menuName = "ScriptableObjects/Spells/ProjectileSpell")]

public class ProjectileSpell : Spell
{
    public ProjectileSpellGO Prefab;
    public float Speed;
    public float Lifetime;

    public int Damage;

    public float CriticalChance;
    



}
