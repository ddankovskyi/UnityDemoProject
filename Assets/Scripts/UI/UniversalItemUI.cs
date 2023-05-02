using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalItemUI : ItemUI
{
    [SerializeField] Image _icon;
    [SerializeField] Image _background;
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
        _icon.sprite = Game.Get<SpellsManager>().GetSpellById(id).Icon;
        // set _background to spell bg
    }
}
