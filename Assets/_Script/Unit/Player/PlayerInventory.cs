using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryData Data { get; private set; }

    private void Awake()
    {
        Data = new InventoryData(4, 6);
    }
}
