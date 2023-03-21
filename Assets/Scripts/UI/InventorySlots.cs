using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlots : MonoBehaviour
{
    [SerializeField] SpellSlot _spellSlotPrefab;
    public int SlotsCount = 3;

    List<SpellSlot> spellSlots = new List<SpellSlot>();
    void Start()
    {
        for (int i = 0; i < SlotsCount; i++)
        {
            spellSlots.Add(Instantiate(_spellSlotPrefab, transform));
        }


    }
}
