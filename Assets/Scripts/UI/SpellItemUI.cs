using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellItemUI : ItemUI
{
    public override void Init(InventoryItem item)
    {
        base.Init(item);
        Debug.Log($"Create spell with id {item.Id}");


    }
}
