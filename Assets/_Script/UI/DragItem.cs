using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IPointerDownHandler
{
    public UIController uiController;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (uiController.isHoldingItem)
            return;

        uiController.StartHoldingItem(gameObject.GetComponent<ItemUI>());
    }
}