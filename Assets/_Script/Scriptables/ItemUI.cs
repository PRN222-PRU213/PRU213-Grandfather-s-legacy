using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private Vector3 originalPosition;

    public int originalX = 0;
    public int originalY = 0;
    [Header("Data")]
    public ItemData itemData;

    public void Init(ItemData data)
    {
        itemData = data;
        Image iconImage = GetComponent<Image>();
        iconImage.sprite = itemData.icon;
        iconImage.alphaHitTestMinimumThreshold = 0.01f;
        originalPosition = transform.position;
    }
    public void Resize(float slotSize)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(
            itemData.itemShape.width * slotSize,
            itemData.itemShape.height * slotSize
        );
    }

    public void SetOccupied(bool occupied)
    {
        Image iconImage = GetComponent<Image>();
        iconImage.color = occupied ? Color.red : Color.white;
    }

    public void SnapToSlot(DropSlot slot)
    {
        RectTransform itemRect = GetComponent<RectTransform>();
        RectTransform slotRect = slot.GetComponent<RectTransform>();
        transform.position = slotRect.position;
        originalPosition = transform.position;
        originalX = slot.x;
        originalY = slot.y;
    }

    public void ReturnToOriginalSlot()
    {
        transform.position = originalPosition;
    }

    public int Width => itemData.itemShape.width;
    public int Height => itemData.itemShape.height;
}