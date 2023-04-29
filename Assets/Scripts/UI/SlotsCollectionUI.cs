using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsCollectionUI : MonoBehaviour
{
    [SerializeField] InventorySlotUI _slotPrefab;
    public int Capacity = 3;
    public readonly List<InventorySlotUI> Slots = new List<InventorySlotUI>();

    public void Init(string slotIdPrefix) // TODO remake with id list, it shouldn't be responsible for id generation. Also maybe add helper to generate id list by prefix
    {
        for (int i = 0; i < Capacity; i++)
        {
            var slot = Instantiate(_slotPrefab, transform);
            slot.slotId = slotIdPrefix + i;
            Slots.Add(slot);
        }
    }

    public void PutInSlot(ItemUI Item, int slotId) => Slots[slotId].ReceiveItem(Item);
    
}
