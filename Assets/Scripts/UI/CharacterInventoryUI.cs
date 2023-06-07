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

    CharacterInventoryManager _inventory;
    void Start()
    {
        _inventory = Game.Get<CharacterInventoryManager>();
        InitStoredSpells();
        InitWands();
    }
    void InitWands()
    {
        int i = 1;
        _wandSlots.ForEach(wandSlot =>
        {
            wandSlot.slotId = CharacterInventoryManager.WANDS_SLOTS_ID_PREFIX + i++;
            if(_inventory.Get(wandSlot.slotId) is WandItem wandItem)
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
        _storedSpells.Init(_inventory.InventorySlotIds);
        foreach(var slot in _storedSpells.Slots.Values)
        {
            var item = _inventory.Get(slot.slotId);
            if (item != null)
            {
                ItemUI itemUI = Instantiate(_universalItemUIPrefab, transform);
                itemUI.Init(item);
                itemUI.ParentForDragging = _parentForDragging;
                slot.Init(itemUI);
            }
        }
    }
    public void UpdateSlot(string slotId) {

        InventorySlotUI slot;
        _storedSpells.Slots.TryGetValue(slotId, out slot);
        if (slot == null) return;

        var item = _inventory.Get(slotId);
        if (slot.ContainedItem == null && item != null)
        {
            ItemUI itemUI = Instantiate(_universalItemUIPrefab, transform);
            itemUI.Init(item);
            itemUI.ParentForDragging = _parentForDragging;
            slot.Init(itemUI);
        }
    }

}
