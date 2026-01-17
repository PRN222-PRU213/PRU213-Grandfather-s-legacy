using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] public InventorySystem system;
    private CanvasGroup canvasGroup;
    RectTransform rt;
    Vector2 offset;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rt = GetComponent<RectTransform>();
        offset = new Vector2(rt.rect.width / 4, -rt.rect.height / 4);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        ItemUI itemUI = eventData.pointerDrag.GetComponent<ItemUI>();
        system.DragItem(itemUI);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rt.position = eventData.position - offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        ItemUI itemUI = eventData.pointerDrag.GetComponent<ItemUI>();
        system.EndDragItem(itemUI);
    }
}