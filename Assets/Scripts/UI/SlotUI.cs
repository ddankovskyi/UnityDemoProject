using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class SlotUI : MonoBehaviour, IDropHandler
{
    [HideInInspector] public string slotId;
    [HideInInspector] public string inventoryPageId;

    [HideInInspector] public ItemUI ContainedItem;

    IInventory<InventoryItem> _inventory = Game.Get<IInventory<InventoryItem>>();

    public void OnDrop(PointerEventData eventData)
    {
        ItemUI item = eventData.pointerDrag.GetComponent<ItemUI>();
        if (item != null)
            AcceptItem(item);
        else
            Debug.LogWarning("Dropped object is not ItemUI");
    }

    public void Init(ItemUI itemUI, bool checkContent = true)
    {
        if (checkContent)
        {
            if (_inventory.Get(slotId) != itemUI.Item)
            {
                Debug.LogWarning("Provided item doesn't meet expected");
                return;
            }
        }

        itemUI.AssigntToSlot(this);
        ContainedItem = itemUI;
        AcceptItemVisually(itemUI);
        return;

    }

    public bool AcceptItem(ItemUI itemUI)
    {
        if(itemUI == ContainedItem)
        {
            AcceptItemVisually(itemUI);
            return true;
        }

        bool isCompatible = _inventory.CheckCompatibility(slotId, itemUI.Item);
        if (!isCompatible) return false;

        if (_inventory.Place(slotId, itemUI.Item))
        {
            itemUI.AssigntToSlot(this);
            ContainedItem = itemUI;
            AcceptItemVisually(itemUI);
            return true;
        }

        if(_inventory.AddAmount(slotId, itemUI.Item))
        {
            if(itemUI.Item.Amount <= 0)
            {
                Destroy(itemUI.gameObject);
            }
            return true;
        }

        

        bool canBeSwapped = _inventory.CheckCompatibility(itemUI.CurrentSlot.slotId, ContainedItem.Item);
        if (!canBeSwapped) return false;

        var extractedItem = _inventory.Extract(slotId);
        if(extractedItem != ContainedItem.Item)
        {
            Debug.LogWarning("Storage system and UI are desynchronized");
            return false;
        }

        var containedItem = ContainedItem;
        var itemsLastSlot = itemUI.CurrentSlot;

        _inventory.Place(slotId, itemUI.Item);
        itemUI.AssigntToSlot(this);
        ContainedItem = itemUI;
        AcceptItemVisually(itemUI);

        itemsLastSlot.AcceptItem(containedItem);
        return true;
        
    }

    void AcceptItemVisually(ItemUI itemUI)
    {
        var spellTransform = itemUI.transform;
        spellTransform.SetParent(transform);
        spellTransform.localPosition = Vector2.zero;
    }

    public void ReleaseSlot(ItemUI item)
    {
        if(ContainedItem == item)
        {
            ContainedItem = null;
            _inventory.Extract(slotId);
        }
    }

}
