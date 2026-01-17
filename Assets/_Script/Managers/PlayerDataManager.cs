using UnityEngine;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public PlayerData Data { get; private set; }
    public int Money => Data?.money ?? 0;

    protected override void Awake()
    {
        base.Awake();
        Load();
    }

    public void AddMoney(int value)
    {
        if (value <= 0) return;
        Data.money += value;
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

    void Save()
    {
        PlayerPrefs.SetInt("Money", Data.money);
        PlayerPrefs.Save();
    }

    void Load()
    {
        Data = new PlayerData
        {
            money = PlayerPrefs.GetInt("Money", 0)
        };
    }
}
