using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData<ItemType>
{
    public Dictionary<string, ItemType> Items;
    public Dictionary<string, System.Type> TypeSlots;
}
