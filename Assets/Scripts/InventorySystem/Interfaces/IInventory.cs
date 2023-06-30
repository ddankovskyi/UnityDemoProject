using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory<ItemType> where ItemType : IStackableItem
{
    public bool CreateTypeslot(System.Type type, string slotId);
    public void DeleteTypeslot(string slotId);
    public bool CheckCompatibility(string slotId, ItemType item);
    public bool Place(string slotId, ItemType item);
    public bool AddAmount(string slotId, ItemType item);
    public ItemType Extract(string slotId);
    public ItemType ExtractAmount(string slotId, int amount);
    public ItemType Get(string slotId);
}
