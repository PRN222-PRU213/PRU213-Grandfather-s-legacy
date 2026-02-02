using NUnit.Framework.Internal.Filters;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] RectTransform inventoryPlayerFrame;
    [SerializeField] RectTransform inventoryOtherFrame;
    [SerializeField] InventoryView playerUI;
    [SerializeField] InventoryView otherUI;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerDataManager playerManager;

    public event System.Func<ItemUI, DropSlot, bool> OnCanPlace;

    private InventoryViewModel playerVM;
    private InventoryViewModel otherVM;

    private bool isOtherInventory = false;

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
            uiManager.SetUI(!isOtherInventory ? UIManager.state.Inventory : UIManager.state.OtherInventory);

            if (isOtherInventory)
            {
                otherVM.AddItemFormList();
            }

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
        InventoryEvent.OnInitOtherInventory += HandleInitOtherInventory;
        InventoryEvent.OnDestroyOtherInventory += HandleDestroyOtherInventory;
        InventoryEvent.OnAddItem += HandleAddItem;
    }

    void OnDisable()
    {
        InventoryEvent.OnPickItem -= HandlePickItem;
        InventoryEvent.OnDropItem -= HandleDropItem;
        InventoryEvent.OnCanPlace -= HandleCanPlace;
        InventoryEvent.OnRemoveItem -= HandleRemoveItem;
        InventoryEvent.OnInitOtherInventory -= HandleInitOtherInventory;
        InventoryEvent.OnDestroyOtherInventory -= HandleDestroyOtherInventory;
        InventoryEvent.OnAddItem -= HandleAddItem;
    }

    public void Init(InventoryData playerInventory)
    {
        playerVM = new InventoryViewModel(playerInventory);
        playerUI.Bind(inventoryPlayerFrame, playerVM, true);
    }

    public void InitOtherInventory(InventoryData otherSystem)
    {
        otherVM = new InventoryViewModel(otherSystem);
        otherUI.Bind(inventoryOtherFrame, otherVM, false);
    }

    void HandlePickItem(ItemUI item)
    {
        if (item.isBelongPlayer)
        {
            playerVM.RemoveItem(item.itemData, item.originalX, item.originalY);
        }
        else
        {
            otherVM.RemoveItem(item.itemData, item.originalX, item.originalY);
        }

        playerUI.SetRaycast(false);
        otherUI.SetRaycast(false);
    }

    void HandleDropItem(ItemUI item, DropSlot dropSlot)
    {
        if (dropSlot.isBelongPlayer)
        {
            playerVM.PlaceItem(item.itemData, dropSlot.x, dropSlot.y);
            playerUI.SetParent(item.GetComponent<RectTransform>());
        }
        else
        {
            otherVM.PlaceItem(item.itemData, dropSlot.x, dropSlot.y);
            otherUI.SetParent(item.GetComponent<RectTransform>());
        }

        item.isBelongPlayer = dropSlot.isBelongPlayer;
        playerUI.SetRaycast(true);
        otherUI.SetRaycast(true);
    }

    bool HandleCanPlace(ItemUI item, DropSlot dropSlot)
    {
        return dropSlot.isBelongPlayer ?
        playerVM.CanPlace(item.itemData, dropSlot.x, dropSlot.y) :
        otherVM.CanPlace(item.itemData, dropSlot.x, dropSlot.y);
    }

    void HandleRemoveItem()
    {
        playerUI.SetRaycast(true);
        otherUI.SetRaycast(true);
    }

    void HandleInitOtherInventory(InventoryData inventory)
    {
        InitOtherInventory(inventory);
        isOtherInventory = true;
    }

    void HandleDestroyOtherInventory()
    {
        isOtherInventory = false;
        otherUI.UnBind();
        otherVM.Dispose();

        otherVM = null;
    }

    void HandleAddItem(ItemData item)
    {
        playerVM.AddItem(item);
    }
}
