using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class WandUI : ItemUI
{
    [SerializeField] Transform _spellSlotsParent;

    [Inject] UniversalItemUI.Factory _itemFactory;
    [Inject] WandSpellSlotUI.Factory _wandSpellSlotFactory;

    List<WandSpellSlotUI> _spellSlots = new List<WandSpellSlotUI>();
    WandItem _wandData;

    public override InventoryItem Item => _wandData;

    public Wand Wand { get; set; }

    public void Init(WandItem wandData)
    {
        _wandData = wandData;
        for (int i = 0; i < _wandData.Spells.Count; i++)
        {

            WandSpellSlotUI spellSlotUI = _wandSpellSlotFactory.Create();
            spellSlotUI.transform.SetParent(_spellSlotsParent, false);
            spellSlotUI.Init(i);
            spellSlotUI.gameObject.name = "Spell slot " + i;
            spellSlotUI.OnItemChanged += OnSlotStatusChanged;
            _spellSlots.Add(spellSlotUI);

            if (_wandData.Spells[i] != null)
            {
                //UniversalItemUI item = Instantiate(_itemPrefab, transform);
                UniversalItemUI item = _itemFactory.Create();
                item.transform.SetParent(transform, false);
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

    public class Factory : PlaceholderFactory<WandUI> { };


}
