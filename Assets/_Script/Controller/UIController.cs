using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    public bool isHoldingItem = false;
    public ItemUI itemHolding;
    private Vector2 offset;

    void Awake()
    {
        inputManager = InputManager.Instance;
    }
    void Update()
    {
        if (!isHoldingItem) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        itemHolding.GetComponent<RectTransform>().position = mousePos - offset;

        if (inputManager.RemoveItem())
        {
            RemoveHoldingItem();
        }
    }

    public void StartHoldingItem(ItemUI item)
    {
        itemHolding = item;
        RectTransform rt = item.GetComponent<RectTransform>();
        offset = new Vector2(rt.rect.width / 4, -rt.rect.height / 4);

        InventoryEvent.OnPickItem?.Invoke(itemHolding);
        isHoldingItem = true;
    }

    public void StopHoldingItem(DropSlot dropSlot)
    {
        if (!InventoryEvent.OnCanPlace.Invoke(itemHolding, dropSlot))
        {
            Debug.Log("Cannot place here");
            return;
        }

        isHoldingItem = false;
        InventoryEvent.OnDropItem?.Invoke(itemHolding, dropSlot);
        itemHolding.SnapToSlot(dropSlot);

        itemHolding = null;
    }

    public void RemoveHoldingItem()
    {
        InventoryEvent.OnRemoveItem?.Invoke();
        isHoldingItem = false;
        itemHolding.Remove();
        itemHolding = null;
    }
}
