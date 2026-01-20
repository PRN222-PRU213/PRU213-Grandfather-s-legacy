using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    private InventoryViewModel vm;
    [SerializeField] private UIController uiController;

    [Header("Frame")]
    private RectTransform slotFrame;
    private GridLayoutGroup gridLayoutGroup;
    private RectTransform itemFrame;

    private List<DropSlot> listSlot = new List<DropSlot>();

    [Header("Prefab")]
    [SerializeField] private GameObject slotFramePrefab;
    [SerializeField] private GameObject itemFramePrefab;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject blockerPrefab;
    [SerializeField] private GameObject itemUIPrefab;

    public void Bind(Transform parent, InventoryViewModel vm)
    {
        this.vm = vm;
        vm.OnCellChanged += OnCellChange;

        Refresh(parent);
    }

    void Refresh(Transform parent)
    {
        slotFrame = Instantiate(slotFramePrefab, parent).GetComponent<RectTransform>();
        gridLayoutGroup = slotFrame.GetComponent<GridLayoutGroup>();
        itemFrame = Instantiate(itemFramePrefab, parent).GetComponent<RectTransform>();
        Vector2 gridSize = vm.GetGridSize();
        ResizeFrame(gridSize);
        InitSlots(gridSize);
    }

    void ResizeFrame(Vector2 gridSize)
    {
        Vector2 cellSize = gridLayoutGroup.cellSize;

        float width = gridSize.x * cellSize.x + (gridSize.x - 1);
        float height = gridSize.y * cellSize.y + (gridSize.y - 1);

        slotFrame.sizeDelta = new Vector2(width, height);
        itemFrame.sizeDelta = new Vector2(width, height);
    }

    void InitSlots(Vector2 gridSize)
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject slot = Instantiate(itemSlotPrefab, slotFrame);
                slot.name = "ItemSlot_" + x + "_" + y;
                slot.GetComponent<DropSlot>().Init(x, y);
                slot.GetComponent<DropSlot>().uiController = uiController;

                listSlot.Add(slot.GetComponent<DropSlot>());
            }
        }
    }

    public void InitItem(ItemData item, int x, int y)
    {
        GameObject itemObject = Instantiate(itemUIPrefab, itemFrame);
        ItemUI itemUI = itemObject.GetComponent<ItemUI>();

        DragItem dragItem = itemObject.GetComponent<DragItem>();
        dragItem.uiController = uiController;

        itemUI.Init(item, this);
        itemUI.Resize(gridLayoutGroup.cellSize.x);
        itemUI.SnapToSlot(GetSlot(x, y));
    }

    public DropSlot GetSlot(int x, int y)
    {
        for (int i = 0; i < slotFrame.childCount; i++)
        {
            DropSlot slot = slotFrame.GetChild(i).GetComponent<DropSlot>();
            if (slot.x == x && slot.y == y)
            {
                return slot;
            }
        }
        return null;
    }

    public void SetRaycast(bool value)
    {
        if (itemFrame == null)
            return;
        foreach (RectTransform itemUI in itemFrame)
        {
            itemUI.GetComponent<CanvasGroup>().blocksRaycasts = value;
        }
    }

    public bool IsOutsideInventory(RectTransform item)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(itemFrame, item.position, null, out localPos);

        return !itemFrame.rect.Contains(localPos);
    }

    void OnCellChange(int x, int y)
    {
        var slot = GetSlot(x, y);
        slot.SetOccupied(!slot.isOccupied);
    }

    // public Vector2 GetGridSize()
    // {
    //     float numberSlotX = math.floor(slotFrame.rect.width / gridLayoutGroup.cellSize.x);
    //     float numberSlotY = math.floor(slotFrame.rect.height / gridLayoutGroup.cellSize.y);

    //     return new Vector2(numberSlotX, numberSlotY);
    // }

    // public DropSlot InitSlot(int x, int y, bool isBlocker)
    // {
    //     if (!isBlocker)
    //     {
    //         GameObject slot = Instantiate(itemSlotPrefab, slotFrame);
    //         slot.name = "ItemSlot_" + x + "_" + y;
    //         slot.GetComponent<DropSlot>().Init(x, y);
    //         slot.GetComponent<DropSlot>().vm = vm;
    //         return slot.GetComponent<DropSlot>();
    //     }
    //     else
    //     {
    //         GameObject blocker = Instantiate(blockerPrefab, slotFrame);
    //         blocker.name = "Blocker_" + x + "_" + y;
    //         return null;
    //     }
    // }

    // public void SetMoney(int amount)
    // {
    //     moneyLabel.text = $"${amount}";
    // }

    // public void SetCapacity(int containing, int capacity)
    // {
    //     capacityLabel.text = $"{containing}/{capacity}";
    // }

    // public ItemUI InitItem(ItemData item)
    // {
    //     GameObject itemObject = Instantiate(itemUIPrefab, itemFrame);
    //     ItemUI itemUI = itemObject.GetComponent<ItemUI>();

    //     DragItem dragItem = itemObject.GetComponent<DragItem>();
    //     dragItem.vm = vm;

    //     itemUI.Init(item);
    //     itemUI.Resize(gridLayoutGroup.cellSize.x);

    //     return itemUI;
    // }
}
