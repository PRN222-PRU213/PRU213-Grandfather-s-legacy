using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        InventorySystem.Instance.setRaycast(false);
        canvasGroup.alpha = 0.6f;
        ItemUI itemUI = eventData.pointerDrag.GetComponent<ItemUI>();
        InventorySystem.Instance.RemoveItem(itemUI.itemData, itemUI.originalX, itemUI.originalY);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InventorySystem.Instance.setRaycast(true);
        canvasGroup.alpha = 1f;
        RectTransform rectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
        if (InventorySystem.Instance.IsOutsideInventory(rectTransform))
        {
            ItemUI itemUI = eventData.pointerDrag.GetComponent<ItemUI>();
            ItemData itemData = itemUI.itemData;
            InventorySystem.Instance.PlaceItem(itemData, itemUI.originalX, itemUI.originalY);
            itemUI.ReturnToOriginalSlot();
        }
    }
}