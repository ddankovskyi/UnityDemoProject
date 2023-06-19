using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Light", menuName = "ScriptableObjects/Spells/Light")]

public class LightEffect : ModifierSpell
{

    public override void Apply(ProjectileSpellGO projectileSpell, ProjectileSpellGO previousSpell)
    {
        Light light = projectileSpell.gameObject.AddComponent<Light>();
        light.type = LightType.Point;
        light.range = 3;
        light.intensity = 5;
        light.color = new Color(0.6981132f, 0.5835067f, 0.2996618f);
    }

}
