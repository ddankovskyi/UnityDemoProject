using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] Transform _parentForDragging;
    SpellSlot _currentSlot;
    SpellSlot _lastSlot;
    Camera _camera;
    CanvasGroup _canvasGroup;
    private void Start()
    {
        _camera = Camera.main;
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        transform.SetParent(_parentForDragging);
        _lastSlot = _currentSlot;
        _currentSlot = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);  // TODO Change to new Input system
        mousePos.z = 0f;
        transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        if(_currentSlot == null)
        {
            _lastSlot.AcceptSpell(this);
        }
    }

    public void AssigntToSlot(SpellSlot slot) => _currentSlot = slot;

}
