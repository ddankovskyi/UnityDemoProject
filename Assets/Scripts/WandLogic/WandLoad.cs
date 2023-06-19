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
    public List<ProjectileSpell> Spells = new List<ProjectileSpell>();

    public float Spread;

    public bool IsUsingFormation;
    public float FormationAngle;

    public float CastDeley;
    public float RechargeTime;

    public void Release(Vector3 position, Quaternion rotation)
    {
        if (Spells.Count == 0)
        {
            Debug.Log("Empty load");
            return;
        }

        ProjectileSpellGO previousProjectile = null; // required for arcs
        float formationAngleStep = 0;
        float nextSpellAngle = 0;
        if (IsUsingFormation)
        {
            formationAngleStep = FormationAngle / Spells.Count;
            nextSpellAngle = -FormationAngle / 2;
        }


        foreach (ProjectileSpell spell in Spells)
        {
            ProjectileSpellGO prefab = Game.Get<SpellsManager>().GetSpellById<ProjectileSpell>(spell.Id).Prefab;
            ProjectileSpellGO spellProjectile = Object.Instantiate(prefab, position, rotation);
            spellProjectile.InitSpell(spell);
            Transform projectileTransform = spellProjectile.transform;
            if (IsUsingFormation)
            {
                projectileTransform.Rotate(new Vector3(0, nextSpellAngle, 0));
                nextSpellAngle += formationAngleStep;
            }
            else
            {
                var randomAngle = Random.Range(-Spread/2, Spread/2);
                projectileTransform.Rotate(new Vector3(0, randomAngle, 0));
            }

            foreach (var modifier in Modifyers)
            {
                modifier.Apply(spellProjectile, previousProjectile);
            }

            previousProjectile = spellProjectile;
        }
    }
}


