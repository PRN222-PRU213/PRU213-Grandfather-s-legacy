using UnityEngine;

public class InventoryGrid
{
    private bool[,] itemGrid;

    public event System.Action<int, int> OnCellChanged;

    public InventoryGrid(Vector2 gridSize)
    {
        itemGrid = new bool[(int)gridSize.x, (int)gridSize.y];
    }

    public void InitSlot(int x, int y, DropSlot slot, bool isBlocker)
    {
        if (isBlocker)
        {
            itemGrid[x, y] = false;

        }
        else
        {
            itemGrid[x, y] = true;

        }
    }

    public bool CanPlace(ItemData item, int startX, int startY)
    {
        var shape = item.itemShape;

        for (int y = 0; y < shape.height; y++)
        {
            for (int x = 0; x < shape.width; x++)
            {
                if (!shape.Occupies(x, y))
                    continue;

                int gx = startX + x;
                int gy = startY + y;

                if (!InSlotFrame(gx, gy))
                    return false;

                if (IsOccupied(gx, gy))
                    return false;
            }
        }
        return true;
    }

    public void SetOccupiedSlot(ItemData item, int startX, int startY, bool isPlace)
    {
        var shape = item.itemShape;

        for (int y = 0; y < shape.height; y++)
        {
            for (int x = 0; x < shape.width; x++)
            {
                if (!shape.Occupies(x, y))
                    continue;

                int gx = startX + x;
                int gy = startY + y;

                OnCellChanged?.Invoke(gx, gy);

                itemGrid[gx, gy] = isPlace;
            }
        }
    }
    //==================================================
    bool InSlotFrame(int x, int y)
    {
        if (x < 0 || y < 0 || x >= itemGrid.GetLength(0) || y >= itemGrid.GetLength(1))
            return false;
        return true;
    }

    bool IsOccupied(int x, int y)
    {
        return itemGrid[x, y];
    }
}