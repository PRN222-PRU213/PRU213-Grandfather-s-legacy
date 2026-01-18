using UnityEngine;

public class FishingManager : Singleton<FishingManager>
{
    public enum FishingState
    {
        Idle,
        Playing,
        CaughtFish,
        Failed
    }

    public float difficultyMultiplier;
    public float successZoneSize;
    public int totalRounds;
    public float trackSpeed;
    public int currentRound = 0;

    [Header("Stat Settings")]

    public float MAX_TRACK_SPEED = 150f;      // Tốc độ di chuyển thanh
    public float MIN_TRACK_SPEED = 100f;

    public float MAX_SUCCESS_ZONE_SIZE = 30;   // Kích thước vùng bắt cá lớn nhất
    public float MIN_SUCCESS_ZONE_SIZE = 25;   // Kích thước vùng bắt cá nhỏ nhất
    public int MAX_ROUNDS = 7;
    public int MIN_ROUNDS = 2;

    [Header("State")]
    public bool canFish = false;
    public ItemData item;
    public FishingState state = FishingState.Idle;


    protected override void Awake()
    {
        base.Awake();
    }

    public void InitStat(ItemData item)
    {
        difficultyMultiplier = Mathf.Clamp01(item.weight / item.MAX_WEIGHT * 0.3f +
                                            item.value / item.MAX_VALUE * 0.7f);
        totalRounds = GetTotalRounds();
        InitMatch();
    }

    public void InitMatch()
    {
        currentRound = 0;
    }

    public void ResetStat()
    {
        trackSpeed = GetTrackSpeed();
        successZoneSize = GetSuccessZoneSize();
    }

    public void SetCanFish(bool value, ItemData item)
    {
        canFish = value;
        this.item = item;

        if (value)
        {
            InitStat(item);
        }
    }

    public bool isDone()
    {
        return currentRound >= totalRounds;
    }

    float GetTrackSpeed()
    {
        return Random.Range(MIN_TRACK_SPEED * (1 + difficultyMultiplier), MAX_TRACK_SPEED * (1 + difficultyMultiplier));
    }

    float GetSuccessZoneSize()
    {
        return Random.Range(MIN_SUCCESS_ZONE_SIZE * (1 - difficultyMultiplier), MAX_SUCCESS_ZONE_SIZE * (1 - difficultyMultiplier));
    }

    int GetTotalRounds()
    {
        return Mathf.RoundToInt(Mathf.Lerp(MIN_ROUNDS, MAX_ROUNDS, difficultyMultiplier));
    }
}
