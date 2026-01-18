using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryManager manager;
    [SerializeField] private InventoryUI ui;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerDataManager playerDataManager;

    private bool isHoldingItem = false;
    private ItemUI itemHolding;
    private Vector2 offset;

    void Awake()
    {
        manager = InventoryManager.Instance;
        uiManager = UIManager.Instance;
        inputManager = InputManager.Instance;
        playerDataManager = PlayerDataManager.Instance;
        Init();
    }

    void Update()
    {
        if (isHoldingItem)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            itemHolding.GetComponent<RectTransform>().position = mousePos - offset;
        }

        if (inputManager.OpenInventory())
        {
            uiManager.SetUI(UIManager.state.Inventory);
            inputManager.EnableUIInput(true);
        }

        if (inputManager.CloseInventory())
        {
            uiManager.SetUI(UIManager.state.None);
            inputManager.EnableUIInput(false);
        }

        if (inputManager.RotateItem())
        {
            Debug.Log("Rotate Item");
        }

        if (inputManager.RemoveItem())
        {
            RemoveItem();
        }
    }

    public void Init()
    {
        Vector2 gridSize = ui.GetGridSize();
        manager.InitGrid(gridSize);
        int count = 0;

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                DropSlot slot = ui.InitSlot(x, y, manager.cells[y, x] == 0);
                manager.InitSlot(x, y, slot);
                if (manager.cells[y, x] == 1)
                    count++;
            }
        }
        playerDataManager.SetCapacity(count);
        ui.SetMoney(playerDataManager.Money);
        ui.SetCapacity(playerDataManager.Containing, playerDataManager.Capacity);
    }

    public void AddItem(ItemData itemData)
    {
        playerDataManager.AddContaining(itemData.itemShape.GetTotalCells());
        ui.SetCapacity(playerDataManager.Containing, playerDataManager.Capacity);
        Debug.Log("Add Item: " + playerDataManager.Capacity);


        for (int y = 0; y < manager.gridSize.y; y++)
        {
            for (int x = 0; x < manager.gridSize.x; x++)
            {
                if (manager.CanPlace(itemData, x, y))
                {
                    manager.SetOccupiedSlot(itemData, x, y, true);
                    ItemUI itemUI = ui.InitItem(itemData);
                    itemUI.SnapToSlot(manager.slots[x, y]);
                    return;
                }
            }
        }
    }

    public void DragItem(GameObject item)
    {
        if (isHoldingItem) return;

        itemHolding = item.GetComponent<ItemUI>();
        itemHolding.GetComponent<CanvasGroup>().alpha = 0.6f;
        RectTransform rt = item.GetComponent<RectTransform>();
        offset = new Vector2(rt.rect.width / 4, -rt.rect.height / 4);
        isHoldingItem = true;

        ui.SetRaycast(false);
        manager.SetOccupiedSlot(itemHolding.itemData, itemHolding.originalX, itemHolding.originalY, false);
    }

    public void DropItem(int x, int y)
    {
        if (!isHoldingItem)
            return;

        ItemData itemData = itemHolding.itemData;
        if (manager.CanPlace(itemData, x, y))
        {
            itemHolding.GetComponent<CanvasGroup>().alpha = 1f;
            ui.SetRaycast(true);
            manager.SetOccupiedSlot(itemData, x, y, true);
            isHoldingItem = false;
            itemHolding.SnapToSlot(manager.slots[x, y]);
        }
    }

    public void RemoveItem()
    {
        if (!isHoldingItem)
            return;

        ui.SetRaycast(true);
        manager.SetOccupiedSlot(itemHolding.itemData, itemHolding.originalX, itemHolding.originalY, false);
        isHoldingItem = false;
        itemHolding.Remove();
    }
}
