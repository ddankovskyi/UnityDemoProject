using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventorySlotUI : SlotUI
{
    [HideInInspector] public string slotId;

    private void Start()
    {
        _inventory = Game.Get<IInventory<InventoryItem>>();
    }


    IInventory<InventoryItem> _inventory;
    public void Init(ItemUI itemUI, bool checkContent = true)
    {
        if(_inventory == null) _inventory = Game.Get<IInventory<InventoryItem>>(); // TODO better solution? 
        if (checkContent)
        {
            InventoryItem expected = _inventory.Get(slotId);
            if (expected != itemUI.Item)
            {
                Debug.LogWarning($"Slot {slotId}: Provided item doesn't meet expected");
                return;
            }
        }
        AcceptItem(itemUI);
        return;

    }

    public override bool CheckIfCanAcceptBySwaping(ItemUI itemUI)
    {
        return _inventory.CheckCompatibility(slotId, itemUI.Item);
    }

    public override bool ReceiveItem(ItemUI itemUI)
    {
        if (itemUI == ContainedItem)
        {
            AcceptItem(itemUI);
            return true;
        }

        bool isCompatible = _inventory.CheckCompatibility(slotId, itemUI.Item);
        if (!isCompatible) return false;

        if (_inventory.Place(slotId, itemUI.Item))
        {
            AcceptItem(itemUI);
            return true;
        }

        if (_inventory.AddAmount(slotId, itemUI.Item))
        {
            if (itemUI.Item.Amount <= 0)
            {
                Destroy(itemUI.gameObject);
            }
            return true;
        }


        if(ContainedItem != null)
        {
            bool canBeSwapped = itemUI.CurrentSlot.CheckIfCanAcceptBySwaping(ContainedItem);
            if (!canBeSwapped) return false;

            var extractedItem = _inventory.Extract(slotId);
            if (extractedItem != ContainedItem.Item)
            {
                Debug.LogWarning($"Storage system and UI are desynchronized. Expexted item id {extractedItem.Id}. Actual item id {ContainedItem.Item.Id}");
                return false;
            }

            var containedItem = ContainedItem;
            var itemsLastSlot = itemUI.CurrentSlot;

            _inventory.Place(slotId, itemUI.Item);
            AcceptItem(itemUI);

            itemsLastSlot.ReceiveItem(containedItem);
            return true;
        }

        _inventory.Place(slotId, itemUI.Item);
        AcceptItem(itemUI);
        return true;

    }


    protected override void ReleaseSlot()
    {
        base.ReleaseSlot();
        _inventory.Extract(slotId);
    }

}
