using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] public InventorySystem system;
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
            system.DropItem(itemUI, x, y);
        }
    }

    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
        Image iconImage = this.GetComponent<Image>();
        iconImage.color = occupied ? Color.red : Color.black;
    }
}
