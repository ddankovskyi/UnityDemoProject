using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CharacterInventoryManager : InventoryManager<InventoryItem>
{
    public const string INVENTORY_SLOTS_ID_PREFIX = "MainInventory_";
    public const string WANDS_SLOTS_ID_PREFIX = "Wand_";

    public CharacterInventoryManager(InventoryData<InventoryItem> data) : base(data) { }

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

    public WandItem GetWandBySlotNumber(int slotNumber)
    {
        return Get(WANDS_SLOTS_ID_PREFIX + slotNumber) as WandItem;
    } 

    public List<string> InventorySlotIds { get; private set; }

    [Inject]
    public void Init(Storage storage)
    {
        InventoryCapasity = storage.Get<CharacterData>().InventorySize;
        InventorySlotIds = Enumerable.Range(0, InventoryCapasity)
            .Select(i => INVENTORY_SLOTS_ID_PREFIX + i)
            .ToList();
    }

}
