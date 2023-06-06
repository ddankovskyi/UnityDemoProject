using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Damage
{
    public int DamageValue { get; private set; }

    public Damage(int damageValue)
    {
        this.DamageValue = damageValue;
    }
    // damage type

}
