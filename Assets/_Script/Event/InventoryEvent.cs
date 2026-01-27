using System;

public static class InventoryEvent
{
    public static Action<ItemUI> OnPickItem;
    public static Action<ItemUI, DropSlot> OnDropItem;
    public static Action<ItemUI> OnRotateItem;
    public static Action OnRemoveItem;

    public static Action<InventoryData> OnInitOtherInventory;
    public static Action OnDestroyOtherInventory;

    public static Func<ItemUI, DropSlot, bool> OnCanPlace;
}
