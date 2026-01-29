using System;
using UnityEngine;

public class TimingBarViewModel : IFishingMinigame
{
    private FishingModel config;

    public float difficultyMultiplier;
    public float successZoneSize;
    public int totalRounds;
    public float trackSpeed;
    public int currentRound = 0;
    public ItemData award;

    public event Action OnStart;
    public event Action<bool> OnHandle;
    public event Action OnFinish;

    public TimingBarViewModel(ItemData item, float difficultyMultiplier)
    {
        config = new FishingModel();
        this.difficultyMultiplier = difficultyMultiplier;
        award = item;

        totalRounds = GetTotalRounds();
        currentRound = 0;
    }

    public void Start()
    {
        successZoneSize = GetSuccessZoneSize();
        trackSpeed = GetTrackSpeed();

        OnStart?.Invoke();
    }
    public bool Handle(bool result)
    {
        if (result) currentRound++;
        OnHandle?.Invoke(result);
        trackSpeed = GetTrackSpeed();

        if (currentRound >= totalRounds)
        {
            OnFinish?.Invoke();
            return true;
        }
        return false;
    }

    float GetTrackSpeed()
    {
        return UnityEngine.Random.Range(config.MIN_TRACK_SPEED * (1 + difficultyMultiplier), config.MAX_TRACK_SPEED * (1 + difficultyMultiplier));
    }

    float GetSuccessZoneSize()
    {
        return UnityEngine.Random.Range(config.MIN_SUCCESS_ZONE_SIZE * (1 - difficultyMultiplier), config.MAX_SUCCESS_ZONE_SIZE * (1 - difficultyMultiplier));
    }

    int GetTotalRounds()
    {
        return Mathf.RoundToInt(Mathf.Lerp(config.MIN_ROUNDS, config.MAX_ROUNDS, difficultyMultiplier));
    }

    public bool IsSuccess(float trackAngle, float successBeginAngle, float zoneRange)
    {
        float successEndAngle = CaculatorAngle(successBeginAngle, zoneRange);

        bool result = IsAngleInRange(trackAngle, successEndAngle, successBeginAngle);
        return result;
    }

    float CaculatorAngle(float angle, float range)
    {
        return (angle - range + 360f) % 360f;
    }

    bool IsAngleInRange(float target, float start, float end)
    {
        target = (target + 360f) % 360f;
        start = (start + 360f) % 360f;
        end = (end + 360f) % 360f;

        float t = (target - start + 360f) % 360f;
        float r = (end - start + 360f) % 360f;

        return t <= r;
    }

}
