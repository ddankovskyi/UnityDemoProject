using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class DropZoneUI : MonoBehaviour, IDropHandler
{

    Vector3 _dropPosShift = new Vector3(0,0,2);
    Transform _character;
    SpellItemObject.Factory _spellItemObjectFactory;
    
    [Inject]
    void Init(CharacterManager characterManager, SpellItemObject.Factory spellItemObjectFactory)
    {
        _character = characterManager.CurrentCharacter.transform;
        _spellItemObjectFactory = spellItemObjectFactory;
    }
  
    public void OnDrop(PointerEventData eventData)
    {
        ItemUI itemUI = eventData.pointerDrag.GetComponent<ItemUI>();
        if (itemUI != null)
        {
            itemUI.CurrentSlot.ReleaseSlot(itemUI);
            //eventData.pointerDrag.SetActive(false); // TODO : drop item instead
            if (itemUI.Item is SpellItem spellItem)
            {
                Vector3 dropPose = _character.position + _dropPosShift;

                _dropPosShift = Quaternion.Euler(0, 95, 0) * _dropPosShift;
                var spellObject = _spellItemObjectFactory.Create();
                spellObject.transform.position = dropPose;
                spellObject.Init(spellItem);
                Destroy(eventData.pointerDrag);
            }
        } 
    }
}
