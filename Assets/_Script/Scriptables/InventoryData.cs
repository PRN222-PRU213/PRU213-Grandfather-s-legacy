using System.Collections.Generic;

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

    public void AddListItem(InventoryItemData item)
    {
        items.Add(item);
    }
}
