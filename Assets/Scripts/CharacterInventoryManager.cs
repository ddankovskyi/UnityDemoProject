using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterInventoryManager : InventoryManager<InventoryItem>
{
    public const string INVENTORY_SLOTS_ID_PREFIX = "MainInventory_";
    public const string WANDS_SLOTS_ID_PREFIX = "Wand_";

    public int InventoryCapasity { get; private set; }

    public string PutInFirstFreeSlot(InventoryItem item)
    {
        for(int i = 0; i < InventoryCapasity; i++)
        {
            if (Place(InventorySlotIds[i], item))
            {
                return InventorySlotIds[i];
            }    
        }
        return null;
    }

    public List<string> InventorySlotIds { get; private set; }

    public override void Init()
    {
        InventoryCapasity = Game.Get<CharacterManager>().InventoryCapasity;
        InventorySlotIds = Enumerable.Range(0, InventoryCapasity)
            .Select(i => INVENTORY_SLOTS_ID_PREFIX + i)
            .ToList();
    }

}
