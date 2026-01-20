using UnityEngine;

public class InventoryViewModel
{
    private InventoryGrid model;
    private InventoryData data;

    public event System.Action<int, int> OnCellChanged
    {
        add { model.OnCellChanged += value; }
        remove { model.OnCellChanged -= value; }
    }

    public InventoryViewModel(InventoryData data)
    {
        this.data = data;
        model = new InventoryGrid(new Vector2(data.column, data.row));
    }

    public void AddItem(ItemData itemData, int x, int y)
    {
        var item = new InventoryItemData
        {
            itemData = itemData,
            position = new Vector2Int(x, y)
        };
        data.items.Add(item);

        model.SetOccupiedSlot(itemData, x, y, true);
    }

    public void RemoveItem(ItemData itemData, int x, int y)
    {
        var item = data.items.Find(i => i.position == new Vector2(x, y));
        model.SetOccupiedSlot(itemData, item.position.x, item.position.y, false);
        data.items.Remove(item);
    }

    public bool CanPlace(ItemData itemData, int x, int y)
    {
        return model.CanPlace(itemData, x, y);
    }

    public Vector2 GetGridSize()
    {
        return new Vector2(data.column, data.row);
    }

    public Vector2 FindPlaceForItem(ItemData itemData)
    {
        for (int y = 0; y <= data.row - itemData.itemShape.height; y++)
        {
            for (int x = 0; x <= data.column - itemData.itemShape.width; x++)
            {
                if (model.CanPlace(itemData, x, y))
                {
                    return new Vector2(x, y);
                }
            }
        }
        return Vector2.negativeInfinity;
    }
}
