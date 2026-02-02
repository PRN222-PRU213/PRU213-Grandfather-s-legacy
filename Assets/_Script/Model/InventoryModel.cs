using UnityEngine;

public class InventoryModel
{
    private bool[,] itemGrid;

    public bool wasOpen = false;
    public bool GetCell(int x, int y) => itemGrid[x, y];
    public void SetCell(int x, int y, bool value) => itemGrid[x, y] = value;

    public InventoryModel(Vector2 gridSize)
    {
        itemGrid = new bool[(int)gridSize.x, (int)gridSize.y];
    }

    public bool IsInBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < itemGrid.GetLength(0) && y < itemGrid.GetLength(1);
    }
}