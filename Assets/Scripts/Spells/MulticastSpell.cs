using UnityEngine;

[CreateAssetMenu(fileName = "MulticastSpell", menuName = "ScriptableObjects/Spells/MulticastSpell")]

class MulticastSpell : Spell
{
    public int SpellsQuantity;
    public bool IsUsingFormation;
    public float FormationAngle;

}

