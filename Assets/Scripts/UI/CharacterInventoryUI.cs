using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryUI : MonoBehaviour
{

    [SerializeField] Transform _parentForDragging;
    [SerializeField] SlotsCollectionUI _storedSpells;
    [SerializeField] List<InventorySlotUI> _wandSlots;
    [SerializeField] WandUI _wandUIPrefab;
    [SerializeField] UniversalItemUI _universalItemUIPrefab;

    IInventory<InventoryItem> _inventory;
    void Start()
    {
        _inventory = Game.Get<IInventory<InventoryItem>>();
        InitStoredSpells();
        InitWands();
    }
    void InitWands()
    {
        var inventory = Game.Get<IInventory<InventoryItem>>();
        int i = 1;
        _wandSlots.ForEach(wandSlot =>
        {
            wandSlot.slotId = InventoryIds.WANDS_SLOTS_ID_PREFIX + i++;
            if(inventory.Get(wandSlot.slotId) is WandItem wandItem)
            {
                WandUI wandUI = Instantiate(_wandUIPrefab, wandSlot.transform);
                wandUI.ParentForDragging = _parentForDragging;
                wandUI.Init(wandItem);
                wandSlot.Init(wandUI);
            }
        });
    }


    void InitStoredSpells()
    {
        _storedSpells.Capacity = Game.Get<CharacterManager>().InventorySize;
        _storedSpells.Init(InventoryIds.INVENTORY_SLOTS_ID_PREFIX);
        _storedSpells.Slots.ForEach(slot =>
        {
            var item = _inventory.Get(slot.slotId);
            if (item != null)
            {
                ItemUI itemUI = Instantiate(_universalItemUIPrefab, transform);
                itemUI.Init(item);
                itemUI.ParentForDragging = _parentForDragging;
                slot.Init(itemUI);
            }
        });
    } 
}
