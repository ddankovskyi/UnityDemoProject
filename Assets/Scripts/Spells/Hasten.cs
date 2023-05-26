using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hasten", menuName = "ScriptableObjects/Spells/Hasten")]

public class Hasten : ModifierSpell
{
    public override void Apply(ProjectileSpellGO projectileSpell, ProjectileSpellGO previousSpell)
    {
        Debug.Log("Apply hasten");
        // Do Nothing
    }


    
}
