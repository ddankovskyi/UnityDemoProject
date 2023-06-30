using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SlotsCollectionUI : MonoBehaviour
{
    public readonly Dictionary<string, InventorySlotUI> Slots = new Dictionary<string, InventorySlotUI> ();
    [Inject] InventorySlotUI.Factory _slotFactory;
    public void Init(List<string> slotIds)  {
        slotIds.ForEach(slotId =>
        {
            var slot = _slotFactory.Create();
            slot.transform.SetParent(transform, false);
            slot.slotId = slotId;
            Slots.Add(slotId, slot);
        });
        
    }

}
