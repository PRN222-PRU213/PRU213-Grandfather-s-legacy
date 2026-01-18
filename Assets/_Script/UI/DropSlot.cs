using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IPointerDownHandler
//                                              ,IDropHandler
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        system.DropItem(x, y);
    }
    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
        Image iconImage = GetComponent<Image>();
        iconImage.color = occupied ? Color.red : Color.black;
    }
}
