using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SpellItemsObjectsCollector : MonoBehaviour
{
    public UnityEvent<string> OnItemCollecter;
    LinkedList<SpellItemObject> _items = new LinkedList<SpellItemObject>();

    [Inject] CharacterInventoryManager _inventoryManager;

    public void SetObjectAvailable(SpellItemObject spellObject)
    {
        if (_items.Count == 0)
        {
            // OnPickUpAvailable
        }
        _items.AddLast(spellObject);

    }

    public void SetObjectNoMoreAvailable(SpellItemObject spellObject)
    {
        _items.Remove(spellObject);
        if (_items.Count == 0)
        {
            // OnPickUpNotAvailable
        }
    }

    public void Collect()
    {
        if (_items.Count == 0) return;
        var itemObject = _items.Last.Value;
        var slotId = _inventoryManager.PutInFirstFreeSlot(itemObject.SpellItem);
        if(slotId != null )
        {
            itemObject.Collect();
            OnItemCollecter.Invoke(slotId);
            _items.RemoveLast();
        } else
        {
            Debug.Log("Inventory is full");
        }
    }
}
