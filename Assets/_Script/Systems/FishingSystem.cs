using System;
using UnityEngine;

public class FishingSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FishingManager fishingManager;
    [SerializeField] private FishingUI fishingUI;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CameraManager cameraManager;

    void Awake()
    {
        fishingManager = FishingManager.Instance;
        inputManager = InputManager.Instance;
        uiManager = UIManager.Instance;
        cameraManager = CameraManager.Instance;
    }
    void Update()
    {
        if (!fishingManager.canFish) return;

        switch (fishingManager.state)
        {
            case FishingManager.FishingState.Idle:

                if (inputManager.FishPressed())
                {
                    StartFishing(fishingManager.item);
                    inputManager.EnableUIInput(true);
                    fishingManager.state = FishingManager.FishingState.Playing;
                }
                break;

            case FishingManager.FishingState.Playing:

                if (inputManager.CatchFish())
                {
                    if (fishingUI.IsSuccess())
                    {
                        fishingManager.currentRound++;
                        fishingUI.IncreaseProcess(fishingManager.currentRound, fishingManager.totalRounds);
                    }

                    if (!fishingManager.isDone())
                    {
                        fishingManager.ResetStat();
                        fishingUI.EachTurn(fishingManager.successZoneSize);
                        return;
                    }
                    EndFishing(fishingManager.item);
                    fishingManager.state = FishingManager.FishingState.Idle;
                }
                fishingUI.MoveTrack(fishingManager.trackSpeed);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetCanFish(bool value, ItemData item)
    {
        fishingManager.SetCanFish(value, item);
    }

    public void StartFishing(ItemData item)
    {
        uiManager.SetUI(UIManager.state.Fishing);
        cameraManager.EnterFishingView();

        fishingUI.Setup(item, fishingManager.totalRounds, fishingManager.difficultyMultiplier);
        fishingManager.ResetStat();
        fishingUI.EachTurn(fishingManager.successZoneSize);
    }

    public void EndFishing(ItemData item)
    {
        cameraManager.ExitView();
        fishingManager.InitMatch();
        inventoryController.AddItemToPlayer(item);
    }
}
