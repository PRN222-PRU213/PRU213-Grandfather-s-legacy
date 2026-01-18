using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public InventorySystem system;

    [Header("Frame")]
    [SerializeField] private RectTransform slotFrame;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private RectTransform itemFrame;
    [SerializeField] private Text moneyLabel;
    [SerializeField] private Text capacityLabel;

    [Header("Prefab")]
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject blockerPrefab;
    [SerializeField] private GameObject itemUIPrefab;

    public Vector2 GetGridSize()
    {
        float numberSlotX = math.floor(slotFrame.rect.width / gridLayoutGroup.cellSize.x);
        float numberSlotY = math.floor(slotFrame.rect.height / gridLayoutGroup.cellSize.y);

        return new Vector2(numberSlotX, numberSlotY);
    }

    public void SetRaycast(bool value)
    {
        foreach (RectTransform itemUI in itemFrame)
        {
            itemUI.GetComponent<CanvasGroup>().blocksRaycasts = value;
        }
    }

    public DropSlot InitSlot(int x, int y, bool isBlocker)
    {
        if (!isBlocker)
        {
            GameObject slot = Instantiate(itemSlotPrefab, slotFrame);
            slot.name = "ItemSlot_" + x + "_" + y;
            slot.GetComponent<DropSlot>().Init(x, y);
            slot.GetComponent<DropSlot>().system = system;
            return slot.GetComponent<DropSlot>();
        }
        else
        {
            GameObject blocker = Instantiate(blockerPrefab, slotFrame);
            blocker.name = "Blocker_" + x + "_" + y;
            return null;
        }
    }

    public void SetMoney(int amount)
    {
        moneyLabel.text = $"${amount}";
    }

    public void SetCapacity(int containing, int capacity)
    {
        capacityLabel.text = $"{containing}/{capacity}";
    }

    public ItemUI InitItem(ItemData item)
    {
        GameObject itemObject = Instantiate(itemUIPrefab, itemFrame);
        ItemUI itemUI = itemObject.GetComponent<ItemUI>();

        DragItem dragItem = itemObject.GetComponent<DragItem>();
        dragItem.system = system;

        itemUI.Init(item);
        itemUI.Resize(gridLayoutGroup.cellSize.x);

        return itemUI;
    }

    public bool IsOutsideInventory(RectTransform item)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            itemFrame,
            item.position,
            null,
            out localPos
        );

        return !itemFrame.rect.Contains(localPos);
    }
}
