using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField] RectTransform inventoryPlayerFrame;
    [SerializeField] RectTransform inventoryOtherFrame;
    [SerializeField] InventoryUI playerUI;
    [SerializeField] InventoryUI otherUI;

    private InventoryViewModel playerVM;
    private InventoryViewModel otherVM;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerDataManager playerManager;

    void Awake()
    {
        uiManager = UIManager.Instance;
        inputManager = InputManager.Instance;
        playerManager = PlayerDataManager.Instance;

        Init(playerManager.InventoryData);
    }

    void Update()
    {
        if (inputManager.OpenInventory())
        {
            uiManager.SetUI(UIManager.state.Inventory);
            inputManager.EnableUIInput(true);
        }

        if (inputManager.CloseInventory())
        {
            uiManager.SetUI(UIManager.state.None);
            inputManager.EnableUIInput(false);
        }
    }

    void OnEnable()
    {
        InventoryEvent.OnPickItem += HandlePickItem;
        InventoryEvent.OnDropItem += HandleDropItem;
        InventoryEvent.OnCanPlace += HandleCanPlace;
        InventoryEvent.OnRemoveItem += HandleRemoveItem;
    }

    void OnDisable()
    {
        InventoryEvent.OnPickItem -= HandlePickItem;
        InventoryEvent.OnDropItem -= HandleDropItem;
        InventoryEvent.OnCanPlace -= HandleCanPlace;
        InventoryEvent.OnRemoveItem -= HandleRemoveItem;
    }

    public void Init(InventoryData playerInventory)
    {
        playerVM = new InventoryViewModel(playerInventory);
        playerUI.Bind(inventoryPlayerFrame, playerVM);
    }

    public void OpenPlayerChest(InventoryData otherSystem)
    {
        otherVM = new InventoryViewModel(otherSystem);
        otherUI.Bind(inventoryOtherFrame, otherVM);
    }

    public void AddItemToPlayer(ItemData itemData)
    {
        Vector2 place = playerVM.FindPlaceForItem(itemData);
        if (place == Vector2.negativeInfinity)
            return;

        if (playerVM.CanPlace(itemData, (int)place.x, (int)place.y))
        {
            playerVM.AddItem(itemData, (int)place.x, (int)place.y);
            playerUI.InitItem(itemData, (int)place.x, (int)place.y);
        }
    }

    void HandlePickItem(ItemUI item)
    {
        playerVM.RemoveItem(item.itemData, item.originalX, item.originalY);
        playerUI.SetRaycast(false);
        otherUI.SetRaycast(false);
    }

    void HandleDropItem(ItemUI item, DropSlot dropSlot)
    {
        playerVM.AddItem(item.itemData, dropSlot.x, dropSlot.y);
        playerUI.SetRaycast(true);
        otherUI.SetRaycast(true);
    }

    bool HandleCanPlace(ItemUI item, DropSlot dropSlot)
    {
        return playerVM.CanPlace(item.itemData, dropSlot.x, dropSlot.y);
    }

    void HandleRemoveItem()
    {
        playerUI.SetRaycast(true);
        otherUI.SetRaycast(true);
    }
}
