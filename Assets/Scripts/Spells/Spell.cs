
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Spells")]
public abstract class Spell : ScriptableObject
{
    public string Id;
    public Sprite Icon; // For UI and pickable item
    [Space]

    public int ManaCost;
    public float CastDelay;
    public float RechargeTime;
    public float Spread;
}

