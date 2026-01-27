using UnityEngine;

public class InventoryItemData
{
    public ItemData itemData;
    public Vector2Int position;

    public InventoryItemData(ItemData item, Vector2Int pos)
    {
        itemData = item;
        position = pos;
    }
}
