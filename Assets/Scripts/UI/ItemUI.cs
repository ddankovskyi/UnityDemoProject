
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform ParentForDragging;
    public virtual InventoryItem Item { get; private set; }
    public SlotUI CurrentSlot { get; set; }

    Camera _camera;
    CanvasGroup _canvasGroup;
    bool _isBeingTransferred;

    public virtual void Init(InventoryItem item) {
        Item = item;
    }
    private void Start()
    {
        _camera = Camera.main;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void AssigntToSlot(SlotUI slot)
    {
        CurrentSlot?.ReleaseSlot(this);
        CurrentSlot = slot;
        _isBeingTransferred = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isBeingTransferred = true;
        _canvasGroup.blocksRaycasts = false;
        Debug.Log(ParentForDragging?.name);
        transform.SetParent(ParentForDragging);
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
        if (_isBeingTransferred)
        {
            CurrentSlot.ReceiveItem(this);
        }
    }

}
