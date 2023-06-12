using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager<T> : IInventory<T> where T : class, IStackableItem 
{
    InventoryData<T> _data;
    Dictionary<string, T> Items => _data.Items;
    Dictionary<string, System.Type> TypeSlots => _data.TypeSlots;

    public bool CheckCompatibility(string slotId, T item)
    {
        TypeSlots.TryGetValue(slotId, out var existingSlotType);
        return existingSlotType == null || existingSlotType.IsInstanceOfType(item);
    }

    public bool Place(string slotId, T item)
    {
        if (item == null)
        {
            Debug.Log("Null item can't be placed. If you want to remove item use Extract instead");
            return false;
        }

        if (!CheckCompatibility(slotId, item)) return false;

        Items.TryGetValue(slotId, out var existingitem);
        if (existingitem == null)
        {
            Items.Add(slotId, item);
            return true;
        }

        return false;
    }

    public bool AddAmount(string slotId, T item)
    {
        if (!CheckCompatibility(slotId, item)) return false;

        Items.TryGetValue(slotId, out var existingitem);
        if (existingitem == null) return false;

        if (existingitem.GetType() != item.GetType()) return false;

        bool isFullStack = item.StackSize <= 1 || item.Amount >= item.StackSize;
        if (isFullStack) return false;

        int acceptedAmount = Mathf.Min(existingitem.StackSize - existingitem.Amount, item.Amount);
        existingitem.Amount += acceptedAmount;
        item.Amount -= acceptedAmount;
        return true;
    }

    public bool CreateTypeslot(Type type, string slotId)
    {
        TypeSlots.TryGetValue(slotId, out var existingSlotType);
        if(existingSlotType == null)
        {
            TypeSlots.Add(slotId, type);
            return true;
        } else if(existingSlotType == type)
        {
            return true;
        } else
        {
            Debug.Log($"Slot with id {slotId} alsready exist and contain typt {existingSlotType} ({type} was requested)");
            return false;
        }
    }

    public void DeleteTypeslot(string slotId)
    {
        TypeSlots.Remove(slotId);
    }

    public T Extract(string slotId)
    {
        Items.TryGetValue(slotId, out var existingitem);
        Items.Remove(slotId);
        return existingitem;
    }

    public T Get(string slotId)
    {
        Items.TryGetValue(slotId, out var existingitem);
        return existingitem;
    }


    public virtual void Init() { }

    public void DebugPrint()
    {
        foreach (var key in Items.Keys)
        {
            Debug.Log(key);
        }
    }

    public T ExtractAmount(string slotId, int amount)
    {
        throw new NotImplementedException();
    }

    public void Load(InventoryData<T> inventoryData)
    {
        _data = inventoryData;
    }
}
