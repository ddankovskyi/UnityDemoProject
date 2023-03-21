using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellSlot : MonoBehaviour, IDropHandler
{
    bool _isSlotAvailable = true;
    public SpellItem StoredSpellItem;
    public void OnDrop(PointerEventData eventData)
    {
        var spell = eventData.pointerDrag.GetComponent<SpellItem>();
        if (spell) 
            AcceptSpell(spell);
    }

    public bool AcceptSpell(SpellItem spellItem)
    {

        if (!_isSlotAvailable && StoredSpellItem != spellItem) 
            return false;

        StoredSpellItem = spellItem;
        StoredSpellItem.AssigntToSlot(this);

        var spellTransform = spellItem.transform;
        spellTransform.SetParent(transform);
        spellTransform.localPosition = Vector2.zero;

        _isSlotAvailable = false;

        return true;
    }

    /* Maybe it would be better to accept spellItem as an argument
     * and check is it assigned and remove it from children if the slot is still its
     * but maybe it is 
     */
    public void ReleaseSlot()
    {
        StoredSpellItem = null;
        _isSlotAvailable = true;
    }
   
}
