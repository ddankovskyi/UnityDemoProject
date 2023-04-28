using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spells/ProjectileSpellWithTrigger")]

public class ProjectileSpellWithTrigger : ProjectileSpell, IWithTrigger
{
    WandLoad _load;
    public WandLoad Load { get => _load; set { _load = value; } }
}
