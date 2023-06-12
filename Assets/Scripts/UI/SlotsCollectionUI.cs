using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsCollectionUI : MonoBehaviour
{
    [SerializeField] InventorySlotUI _slotPrefab;
    public readonly Dictionary<string, InventorySlotUI> Slots = new Dictionary<string, InventorySlotUI> ();

    public void Init(List<string> slotIds)  {
        slotIds.ForEach(slotId =>
        {
            var slot = Instantiate(_slotPrefab, transform);
            slot.slotId = slotId;
            Slots.Add(slotId, slot);
        });
        
    }

}
