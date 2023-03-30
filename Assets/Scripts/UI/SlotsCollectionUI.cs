using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsCollectionUI : MonoBehaviour
{
    [SerializeField] SlotUI _slotPrefab;
    public int Capacity = 3;
    public readonly List<SlotUI> Slots = new List<SlotUI>();

    public void Init(string slotIdPrefix, Action<string, InventoryItem> onSlotStatusChanged)
    {
        for (int i = 0; i < Capacity; i++)
        {
            var slot = Instantiate(_slotPrefab, transform);
            slot.slotId = slotIdPrefix + i;
            Slots.Add(slot);
        }
    }

    public void PutInSlot(ItemUI Item, int slotId) => Slots[slotId].AcceptItem(Item);
    
}
