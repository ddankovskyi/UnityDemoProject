using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WandUI : ItemUI
{
    [SerializeField] Transform _spellSlotsParent;
    [SerializeField] WandSpellSlotUI _wandSpellSlotPrefab;
    [SerializeField] UniversalItemUI _itemPrefab;

    List<WandSpellSlotUI> _spellSlots = new List<WandSpellSlotUI>();
    WandItem _wandData;

    public override InventoryItem Item => _wandData;

    public Wand Wand { get; set; }

    public void Init(WandItem wandData)
    {
        _wandData = wandData;
        for (int i = 0; i < _wandData.Spells.Count; i++)
        {

            WandSpellSlotUI spellSlotUI = Instantiate(_wandSpellSlotPrefab, _spellSlotsParent);
            spellSlotUI.Init(i);
            spellSlotUI.gameObject.name = "Spell slot " + i;
            spellSlotUI.OnItemChanged += OnSlotStatusChanged;
            _spellSlots.Add(spellSlotUI);

            if (_wandData.Spells[i] != null)
            {
                //UniversalItemUI item = Instantiate(_itemPrefab, transform);
                UniversalItemUI item = Instantiate(_itemPrefab, spellSlotUI.transform);
                item.ParentForDragging = ParentForDragging;
                item.Init(_wandData.Spells[i]);
                spellSlotUI.Init(item);
            }
        }
    }

   


    void OnSlotStatusChanged(int slotId, SpellItem newSpell)
    {
        _wandData.Spells[slotId] = newSpell;
        Wand?.ResetState(); // TODO temp solution, remove later
    }


}
