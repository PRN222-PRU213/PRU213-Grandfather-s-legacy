using System;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryManager manager;
    [SerializeField] private InventoryUI ui;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerDataManager playerDataManager;

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
    }

    public void Init()
    {
        ui.SetMoney(playerDataManager.Money);
        Vector2 gridSize = ui.GetGridSize();
        manager.InitGrid(gridSize);

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                DropSlot slot = ui.InitSlot(x, y, manager.cells[y, x] == 0);
                manager.InitSlot(x, y, slot);
            }
        }
    }

    public void AddItem(ItemData itemData)
    {
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

    public void DragItem(ItemUI itemUI)
    {
        ui.SetRaycast(false);
        manager.SetOccupiedSlot(itemUI.itemData, itemUI.originalX, itemUI.originalY, false);
    }

    public void EndDragItem(ItemUI itemUI)
    {
        ui.SetRaycast(true);
        if (ui.IsOutsideInventory(itemUI.GetComponent<RectTransform>()))
        {
            manager.SetOccupiedSlot(itemUI.itemData, itemUI.originalX, itemUI.originalY, true);
            itemUI.ReturnToOriginalSlot();
        }
    }

    public void DropItem(ItemUI itemUI, int x, int y)
    {
        ItemData itemData = itemUI.itemData;
        if (manager.CanPlace(itemData, x, y))
        {
            manager.SetOccupiedSlot(itemData, x, y, true);
            itemUI.SnapToSlot(manager.slots[x, y]);
        }
        else
        {
            manager.SetOccupiedSlot(itemData, itemUI.originalX, itemUI.originalY, true);
            itemUI.ReturnToOriginalSlot();
        }
    }
}
//     [Header("References")]
//     [SerializeField] private RectTransform inventory;
//     [SerializeField] private RectTransform slotFrame;
//     [SerializeField] private GridLayoutGroup gridLayoutGroup;
//     [SerializeField] private GameObject itemSlotPrefab;
//     [SerializeField] private GameObject blockerPrefab;
//     [SerializeField] private RectTransform itemFrame;
//     [SerializeField] private GameObject itemUIPrefab;

//     private int[,] cells =
//     {
//         {0,0,1,1,1,0,0},
//         {0,1,1,1,1,1,1},
//         {1,1,1,1,1,1,1},
//         {1,1,1,1,1,1,1},
//         {0,1,1,1,1,1,0},
//         {0,0,1,1,1,0,0},
//     };

//     private bool[,] itemGrid;
//     private DropSlot[,] slots;

//     private float numberSlotX;
//     private float numberSlotY;

//     protected override void Awake()
//     {
//         base.Awake();
//     }
//     private void Start()
//     {
//         numberSlotX = math.floor(slotFrame.rect.width / gridLayoutGroup.cellSize.x);
//         numberSlotY = math.floor(slotFrame.rect.height / gridLayoutGroup.cellSize.y);
//         itemGrid = new bool[(int)numberSlotX, (int)numberSlotY];
//         slots = new DropSlot[(int)numberSlotX, (int)numberSlotY];
//         InitializeInventory();
//     }

// void Update()
// {
//     if (GameInput.Instance.OpenInventory())
//     {
//         openUI(true);
//     }

//     if (GameInput.Instance.CloseInventory())
//     {
//         openUI(false);
//     }
// }

//     public void openUI(bool value)
//     {
//         inventory.gameObject.SetActive(value);
//     }

//     public void setRaycast(bool value)
//     {
//         foreach (RectTransform itemUI in itemFrame)
//         {
//             itemUI.GetComponent<CanvasGroup>().blocksRaycasts = value;
//         }
//     }

//     void InitializeInventory()
//     {
//         for (int y = 0; y < numberSlotY; y++)
//         {
//             for (int x = 0; x < numberSlotX; x++)
//             {
//                 if (cells[y, x] == 1)
//                 {
//                     GameObject slot = Instantiate(itemSlotPrefab, slotFrame);
//                     slot.name = "ItemSlot_" + x + "_" + y;
//                     slot.GetComponent<DropSlot>().Init(x, y);
//                     itemGrid[x, y] = false;
//                     slots[x, y] = slot.GetComponent<DropSlot>();
//                 }
//                 else
//                 {
//                     GameObject blocker = Instantiate(blockerPrefab, slotFrame);
//                     blocker.name = "Blocker_" + x + "_" + y;
//                     itemGrid[x, y] = true;
//                 }
//             }
//         }
//     }
//     //==============================================
//     public void AddItem(ItemData data)
//     {
//         GameObject item = Instantiate(itemUIPrefab, itemFrame);
//         ItemUI itemUI = item.GetComponent<ItemUI>();
//         itemUI.Init(data);
//         itemUI.Resize(gridLayoutGroup.cellSize.x);

//         for (int y = 0; y < numberSlotY; y++)
//         {
//             for (int x = 0; x < numberSlotX; x++)
//             {
//                 if (itemGrid[x, y] == false && CanPlace(data, x, y))
//                 {
//                     PlaceItem(data, x, y);
//                     itemUI.SnapToSlot(slots[x, y]);
//                     return;
//                 }
//             }
//         }
//     }

//     bool HasSlot(int x, int y)
//     {
//         if (x < 0 || y < 0 || x >= itemGrid.GetLength(0) || y >= itemGrid.GetLength(1))
//             return false;
//         return true;
//     }

//     bool IsOccupied(int x, int y)
//     {
//         return itemGrid[x, y];
//     }

//     public bool IsOutsideInventory(RectTransform item)
//     {
//         Vector2 localPos;
//         RectTransformUtility.ScreenPointToLocalPointInRectangle(
//             itemFrame,
//             item.position,
//             null,
//             out localPos
//         );

//         return !itemFrame.rect.Contains(localPos);
//     }

//     public bool CanPlace(ItemData item, int startX, int startY)
//     {
//         var shape = item.itemShape;

//         for (int y = 0; y < shape.height; y++)
//         {
//             for (int x = 0; x < shape.width; x++)
//             {
//                 if (!shape.Occupies(x, y))
//                     continue;

//                 int gx = startX + x;
//                 int gy = startY + y;

//                 if (!HasSlot(gx, gy))
//                     return false;

//                 if (IsOccupied(gx, gy))
//                     return false;
//             }
//         }
//         return true;
//     }

//     public void PlaceItem(ItemData item, int startX, int startY)
//     {
//         var shape = item.itemShape;

//         for (int y = 0; y < shape.height; y++)
//         {
//             for (int x = 0; x < shape.width; x++)
//             {
//                 if (!shape.Occupies(x, y))
//                     continue;

//                 int gx = startX + x;
//                 int gy = startY + y;

//                 itemGrid[gx, gy] = true;
//                 slots[gx, gy].SetOccupied(true);
//             }
//         }
//     }

//     public void RemoveItem(ItemData item, int startX, int startY)
//     {
//         var shape = item.itemShape;

//         for (int y = 0; y < shape.height; y++)
//         {
//             for (int x = 0; x < shape.width; x++)
//             {
//                 if (!shape.Occupies(x, y))
//                     continue;

//                 int gx = startX + x;
//                 int gy = startY + y;

//                 itemGrid[gx, gy] = false;
//                 slots[gx, gy].SetOccupied(false);
//             }
//         }
//     }
// }
