using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecycleBin : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<SpellItem>() != null)
        {
            eventData.pointerDrag.SetActive(false); // TODO : drop item instead
        } // else if wand
    }
}
