using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public int x;
    public int y;
    public bool isOccupied = false;

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemUI itemUI = eventData.pointerDrag.GetComponent<ItemUI>();
            ItemData itemData = itemUI.itemData;

            if (InventorySystem.Instance.CanPlace(itemData, x, y))
            {
                InventorySystem.Instance.PlaceItem(itemData, x, y);
                itemUI.SnapToSlot(this);

            }
            else
            {
                InventorySystem.Instance.PlaceItem(itemData, itemUI.originalX, itemUI.originalY);
                itemUI.ReturnToOriginalSlot();
            }
        }
    }

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
        Image iconImage = this.GetComponent<Image>();
        iconImage.color = occupied ? Color.red : Color.black;
    }
}
