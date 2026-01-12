using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform inventoryUI;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private GameObject itemSlotPrefab;

    private void Start()
    {
        InitializeInventory();
    }

    void InitializeInventory()
    {
        float numberSlotX = math.floor(inventoryUI.rect.width / gridLayoutGroup.cellSize.x);
        float numberSlotY = math.floor(inventoryUI.rect.height / gridLayoutGroup.cellSize.y);

        for (int y = 0; y < numberSlotY; y++)
        {
            for (int x = 0; x < numberSlotX; x++)
            {
                GameObject slot = Instantiate(itemSlotPrefab, inventoryUI);
                slot.name = "ItemSlot_" + x + "_" + y;
            }
        }
    }
}
