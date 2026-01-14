using UnityEngine;

[CreateAssetMenu(
    fileName = "NewItemData",
    menuName = "Inventory/Item Data",
    order = 1
)]
public class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    public string itemId;          // ID duy nhất (dùng cho save/load)
    public string itemName;
    [TextArea]
    public string description;

    [Header("Visual")]
    public Sprite icon;

    [Header("Grid Shape")]
    public ItemShape itemShape;

    [Header("Type")]
    public ItemType itemType;
}

public enum ItemType
{
    Consumable,
    Equipment,
    Quest,
    Material
}