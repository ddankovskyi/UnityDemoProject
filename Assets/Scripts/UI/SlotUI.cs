using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class SlotUI : MonoBehaviour, IDropHandler
{
    [HideInInspector] public string slotId;
    [HideInInspector] public string inventoryPageId;

    [HideInInspector] public ItemUI ContainedItem;


    public void OnDrop(PointerEventData eventData)
    {
        ItemUI item = eventData.pointerDrag.GetComponent<ItemUI>();
        if (item != null)
            AcceptItem(item);
        else
            Debug.Log("Dropped object is not ItemUI");
    }

    public bool AcceptItem(ItemUI itemUI)
    {
        if(itemUI == ContainedItem)
        {
            AcceptItemVisually(itemUI);
            return true;
        }
        if (ContainedItem != null)
        {
            return SwapItems(itemUI);
        }

        bool isAcceptable = Game.Get<InventoryManager>().PutItem(inventoryPageId, slotId, itemUI.Item);

        if (!isAcceptable) return false;

        AcceptItemVisually(itemUI);
        return true;
    }

    void AcceptItemVisually(ItemUI itemUI)
    {
        ContainedItem = itemUI;
        ContainedItem.AssigntToSlot(this);

        var spellTransform = itemUI.transform;
        spellTransform.SetParent(transform);
        spellTransform.localPosition = Vector2.zero;
    }

    bool SwapItems(ItemUI itemUI)
    {
        SlotUI otherSlot = itemUI.CurrentSlot;
        bool isAcceptable = Game.Get<InventoryManager>().SwapItems(inventoryPageId, slotId, otherSlot.inventoryPageId, otherSlot.slotId);
        if (!isAcceptable) return false;

        otherSlot.AcceptItemVisually(ContainedItem);
        AcceptItemVisually(itemUI);
        return true;
    }

    public void ReleaseSlot()
    {
        Game.Get<InventoryManager>().RemoveItem(inventoryPageId, slotId);
        ContainedItem = null;
    }


    //public event Action<string, InventoryItem> OnSlotStatusChanghed;

    //private void OnDestroy()
    //{
    //    foreach (Delegate handler in OnSlotStatusChanghed.GetInvocationList())
    //    {
    //        OnSlotStatusChanghed -= (Action<string, InventoryItem>)handler;
    //    }

    //}
}
