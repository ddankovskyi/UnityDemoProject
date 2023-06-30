using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UniversalItemUI : ItemUI
{
    [SerializeField] Image _icon;
    [SerializeField] Image _background;

    SpellsManager _spellsManager;

    [Inject]
    public void Construct(SpellsManager spellsManager)
    {
        _spellsManager = spellsManager;
    }
    public override void Init(InventoryItem item)
    {
        base.Init(item);
        if(item is SpellItem)
        {
            InitSpellItem(item.Id);
        }
    }

    void InitSpellItem(string id)
    {
       
        _icon.sprite = _spellsManager.GetSpellById(id).Icon;
        
        // set _background to spell bg
    }

    public class Factory : PlaceholderFactory<UniversalItemUI> {}
}
