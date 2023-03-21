using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandSpellSlots : MonoBehaviour
{
    [SerializeField] SpellSlot _spellSlotPrefab;
    public int SlotsCount = 3;

    List<SpellSlot> spellSlots = new List<SpellSlot>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SlotsCount; i++)
        {
            spellSlots.Add(Instantiate(_spellSlotPrefab, transform));
        }
    }
}
