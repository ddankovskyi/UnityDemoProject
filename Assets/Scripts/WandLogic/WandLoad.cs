using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Load is a bunch of spells with modifiers and formation data
 * ready to shoot.
 * It is used to shoot spells directly from wand or on spells 
 * with trigger or time trigger
 * 
 */
public class WandLoad
{
    public List<ModifierSpell> Modifyers = new List<ModifierSpell>();
    public List<SpawnableSpell> Spells = new List<SpawnableSpell>();

    public float Spread;

    public bool IsUsingFormation;
    public float FormationAngle;

    public float CastDeley;
    public float RechargeTime;


}
