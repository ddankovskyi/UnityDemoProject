using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecycleBinUI : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<ItemUI>() != null)
        {
            eventData.pointerDrag.SetActive(false); // TODO : drop item instead
        } 
    }
}
