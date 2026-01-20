// using UnityEngine;

// public class InventoryManager : Singleton<InventoryManager>
// {
//     [Header("Data")]
//     public int[,] cells =
//     {
//         {0,0,1,1,1,0,0},
//         {0,1,1,1,1,1,1},
//         {1,1,1,1,1,1,1},
//         {1,1,1,1,1,1,1},
//         {0,1,1,1,1,1,0},
//         {0,0,1,1,1,0,0},
//     };
//     private bool[,] itemGrid;
//     public DropSlot[,] slots;
//     public Vector2 gridSize;

//     protected override void Awake()
//     {
//         base.Awake();
//     }

//     public void InitGrid(Vector2 grid)
//     {
//         gridSize = grid;

//         itemGrid = new bool[(int)gridSize.x, (int)gridSize.y];
//         slots = new DropSlot[(int)gridSize.x, (int)gridSize.y];
//     }

//     public void InitSlot(int x, int y, DropSlot slot)
//     {
//         if (cells[y, x] == 1)
//         {
//             itemGrid[x, y] = false;
//             slots[x, y] = slot;
//         }
//         else
//         {
//             itemGrid[x, y] = true;
//             slots[x, y] = slot;
//         }
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

//                 if (!InSlotFrame(gx, gy))
//                     return false;

//                 if (IsOccupied(gx, gy))
//                     return false;
//             }
//         }
//         return true;
//     }

//     public void SetOccupiedSlot(ItemData item, int startX, int startY, bool isPlace)
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

//                 itemGrid[gx, gy] = isPlace;
//                 slots[gx, gy].SetOccupied(isPlace);
//             }
//         }
//     }
//     //==================================================
//     bool InSlotFrame(int x, int y)
//     {
//         if (x < 0 || y < 0 || x >= itemGrid.GetLength(0) || y >= itemGrid.GetLength(1))
//             return false;
//         return true;
//     }

//     bool IsOccupied(int x, int y)
//     {
//         return itemGrid[x, y];
//     }
// }