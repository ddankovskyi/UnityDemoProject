using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] Transform _parentForDragging;
    [SerializeField] SlotsCollectionUI _storedSpells;
    [SerializeField] List<SlotUI> _wandSlots;

    [SerializeField] SpellItemUI _spellItemUIPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InitStoredSpells();
        InitWands();
    }
    void InitWands()
    {
        string slotIdPrefix = "WandSlot_";
        var inventory = Game.Get<InventoryManager>();
        int i = 0;
        _wandSlots.ForEach(ws =>
        {
            ws.slotId = slotIdPrefix + i;
        });
    }

    int _inventoryCapasity = 10; // TODO where should I store it? 

    void InitStoredSpells()
    {
        var inventory = Game.Get<InventoryManager>();
        _storedSpells.Capacity = _inventoryCapasity;
        _storedSpells.Init("MainInventory_", AcceptInventorySpellSlotStatus);
        _storedSpells.Slots.ForEach(slot =>
        {
            var item = inventory.GetItem(slot.slotId);
            if (item != null)
            {
                SpellItemUI spellItemUI = Instantiate(_spellItemUIPrefab, transform);
                spellItemUI.Init(item.Id);
                spellItemUI.ParentForDragging = _parentForDragging;
                slot.AcceptItem(spellItemUI);
            }
        });
    }

    void AcceptInventorySpellSlotStatus(string slotId, InventoryItem item)
    {

    }

    void AcceptWandSlotStatus(string slotId, InventoryItem item)
    {


    }


}
