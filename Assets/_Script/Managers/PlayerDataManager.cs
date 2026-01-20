using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public PlayerData Data { get; private set; }
    public int Money => Data?.money ?? 0;
    public int Capacity => Data?.capacity ?? 0;
    public int Containing => Data?.containing ?? 0;
    public IReadOnlyList<string> Items => Data.items;
    public InventoryData InventoryData => Data.inventoryData;

    protected override void Awake()
    {
        base.Awake();
        // ResetPlayerData();
        Load();
    }

    void EnsureData()
    {
        if (Data == null)
            Load();
    }

    public void AddMoney(int value)
    {

        if (value <= 0) return;
        Data.money += value;
        Save();
    }

    public void SetCapacity(int value)
    {
        EnsureData();

        if (value < 0) return;
        Data.capacity = value;
        Save();
    }

    public void AddContaining(int value)
    {

        if (value == 0) return;
        Data.containing += value;
        if (Data.containing < 0) Data.containing = 0;
        Save();
    }

    public void AddItem(string itemId)
    {

        if (string.IsNullOrEmpty(itemId)) return;

        Data.items.Add(itemId);
        Save();
    }

    public void RemoveItem(string itemId)
    {
        if (Data.items.Remove(itemId))
            Save();
    }

    public bool SpendMoney(int value)
    {
        if (Data.money < value)
            return false;

        Data.money -= value;
        Save();
        return true;
    }

    [ContextMenu("Reset Player Data")]
    public void ResetPlayerData()
    {
        PlayerPrefs.DeleteKey("PlayerData");
    }

    void Save()
    {
        // PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(Data));
        // PlayerPrefs.Save();
    }

    void Load()
    {
        Data = new PlayerData();

        // if (PlayerPrefs.HasKey("PlayerData"))
        // {
        //     Data = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("PlayerData"));
        // }
        // else
        // {
        //     Data = new PlayerData();
        // }
    }
}
