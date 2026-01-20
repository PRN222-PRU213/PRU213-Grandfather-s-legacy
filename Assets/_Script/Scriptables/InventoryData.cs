using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public int row;
    public int column;
    public int[,] cells;
    public List<InventoryItemData> items = new();

    public InventoryData(int row, int column)
    {
        this.row = row;
        this.column = column;
        cells = new int[column, row];
    }
}
