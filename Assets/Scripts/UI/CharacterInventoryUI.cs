using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryUI : MonoBehaviour
{

    [SerializeField] Transform _parentForDragging;
    [SerializeField] SlotsCollectionUI _storedSpells;
    [SerializeField] List<InventorySlotUI> _wandSlots;
    [SerializeField] ItemsRegistry _itemsRegistry;


    void Start()
    {
        InitStoredSpells();
        InitWands();
    }
    void InitWands()
    {
        var inventory = Game.Get<IInventory<InventoryItem>>();
        int i = 0;
        _wandSlots.ForEach(ws =>
        {
            ws.slotId = InventoryIds.WANDS_SLOTS_ID_PREFIX + i;
        });
    }


    void InitStoredSpells()
    {
        var inventory = Game.Get<IInventory<InventoryItem>>();
        _storedSpells.Capacity = Game.Get<Player>().InventorySize;
        _storedSpells.Init(InventoryIds.INVENTORY_SLOTS_ID_PREFIX);
        _storedSpells.Slots.ForEach(slot =>
        {
            var item = inventory.Get(slot.slotId);
            if (item != null)
            {
                //TODO remake without registry
                ItemUI itemUI = Instantiate(_itemsRegistry.GetPrefabUI(item.Id), transform);
                itemUI.Init(item);
                itemUI.ParentForDragging = _parentForDragging;
                slot.Init(itemUI);
            }
        });
    } 
}
