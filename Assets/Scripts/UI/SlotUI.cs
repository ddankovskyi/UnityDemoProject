using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IDropHandler
{
    [HideInInspector] public ItemUI ContainedItem;

    public void OnDrop(PointerEventData eventData)
    {
        ItemUI item = eventData.pointerDrag.GetComponent<ItemUI>();
        if (item != null)
            ReceiveItem(item);
        else
            Debug.LogWarning("Dropped object is not ItemUI");
    }

    public virtual void Init(ItemUI itemUI)
    {
        AcceptItem(itemUI);
        return;
    }

    public virtual bool CheckIfCanAcceptBySwaping(ItemUI itemUI)
    {
        return true;
    }

    public virtual bool ReceiveItem(ItemUI itemUI)
    {
        if (itemUI == ContainedItem)
        {
            AcceptItem(itemUI);
            return true;
        }

        if(ContainedItem = null)
        {           
            AcceptItem(itemUI);
            return true;
        }

        if (itemUI.CurrentSlot.CheckIfCanAcceptBySwaping(ContainedItem))
        {
            var containedItem = ContainedItem;
            var itemsLastSlot = itemUI.CurrentSlot;

    
            AcceptItem(itemUI);

            itemsLastSlot.ReceiveItem(containedItem);
            return true;
        }

        return false;
    }

    protected virtual void AcceptItem(ItemUI itemUI)
    {
        itemUI.AssigntToSlot(this);
        ContainedItem = itemUI;
        var spellTransform = itemUI.transform;
        spellTransform.SetParent(transform);
        spellTransform.localPosition = Vector2.zero;
    }

    public void ReleaseSlot(ItemUI item)
    {
        if (ContainedItem == item)
            ReleaseSlot();
    }

    protected virtual void ReleaseSlot() { 
        ContainedItem = null; 
    }

}

