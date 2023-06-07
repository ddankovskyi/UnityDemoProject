using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneUI : MonoBehaviour, IDropHandler
{
    [SerializeField] SpellItemObject spellItemObjectPrefab;

    Vector3 _dropPosShift = new Vector3(0,0,2);
  
    public void OnDrop(PointerEventData eventData)
    {
        ItemUI itemUI = eventData.pointerDrag.GetComponent<ItemUI>();
        if (itemUI != null)
        {
            itemUI.CurrentSlot.ReleaseSlot(itemUI);
            //eventData.pointerDrag.SetActive(false); // TODO : drop item instead
            if (itemUI.Item is SpellItem spellItem)
            {
                Vector3 dropPose = Game.Get<CharacterManager>().CurrentCharacter.transform.position + _dropPosShift;

                _dropPosShift = Quaternion.Euler(0, 95, 0) * _dropPosShift;
                var spellObject = Instantiate(spellItemObjectPrefab, dropPose, Quaternion.identity);
                spellObject.Init(spellItem);
            }
            Destroy(eventData.pointerDrag);
        } 
    }
}
