using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Damage
{
    public float DamageValue { get; private set; }

    public Damage(float damageValue)
    {
        this.DamageValue = damageValue;
    }
    // damage type
}
