using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryUI : MonoBehaviour
{

    [SerializeField] Transform _parentForDragging;
    [SerializeField] SlotsCollectionUI _storedSpells;
    [SerializeField] List<SlotUI> _wandSlots;
    [SerializeField] ItemsRegistry _itemsRegistry;

    // Start is called before the first frame update

    public const string INVENTORY_SLOTS_ID_PREFIX = "MainInventory_"; // TODO you probably don't want to store it here 
    void Start()
    {
        InitStoredSpells();
        InitWands();
    }
    void InitWands()
    {
        string slotIdPrefix = "WandSlot_";
        var inventory = Game.Get<IInventory<InventoryItem>>();
        int i = 0;
        _wandSlots.ForEach(ws =>
        {
            ws.slotId = slotIdPrefix + i;
        });
    }


    void InitStoredSpells()
    {
        var inventory = Game.Get<IInventory<InventoryItem>>();
        _storedSpells.Capacity = Game.Get<Player>().InventorySize;
        _storedSpells.Init(INVENTORY_SLOTS_ID_PREFIX);
        _storedSpells.Slots.ForEach(slot =>
        {
            var item = inventory.Get(slot.slotId);
            if (item != null)
            {
                ItemUI itemUI = Instantiate(_itemsRegistry.GetPrefabUI(item.Id), transform);
                itemUI.Init(item);
                itemUI.ParentForDragging = _parentForDragging;
                slot.Init(itemUI);
            }
        });
    } 
}
