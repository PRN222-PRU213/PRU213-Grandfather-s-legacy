using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragItem : MonoBehaviour, IPointerDownHandler
// IDragHandler, IEndDragHandler, IBeginDragHandler, 
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        system.DragItem(gameObject);
    }
}