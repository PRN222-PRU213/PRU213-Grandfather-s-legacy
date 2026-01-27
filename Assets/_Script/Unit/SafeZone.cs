using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public bool isActive = true;
    public bool ditectedPlayer = false;

    [SerializeField] private ItemData item;

    private InventoryData inventory;

    void Awake()
    {
        inventory = new InventoryData(5, 5);
        InventoryItemData item = new InventoryItemData(this.item, new Vector2Int(1, 2));

        inventory.AddListItem(item);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (!other.CompareTag("Player")) return;

        InventoryEvent.OnInitOtherInventory?.Invoke(inventory);


        ditectedPlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isActive) return;
        if (!other.CompareTag("Player")) return;

        InventoryEvent.OnDestroyOtherInventory?.Invoke();

        ditectedPlayer = false;
    }
}
