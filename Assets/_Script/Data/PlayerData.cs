using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int money;
    public int capacity;
    public int containing;
    public List<string> items = new();

    public InventoryData inventoryData = new InventoryData();
}