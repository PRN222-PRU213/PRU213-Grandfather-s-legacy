using UnityEngine;

public class FishingController : MonoBehaviour
{
    [SerializeField] TimingBarView timingBarView;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private UIManager uiManager;

    private IFishingMinigame vm;
    private ItemData item;
    private bool canFishing = false;

    void Awake()
    {
        inputManager = InputManager.Instance;
    }

    void OnEnable()
    {
        FishingEvent.OnEnableFishing += HandleEnableFishing;
        FishingEvent.OnUnableFishing += HandleUnableFishing;
    }

    void OnDisable()
    {
        FishingEvent.OnEnableFishing -= HandleEnableFishing;
        FishingEvent.OnUnableFishing -= HandleUnableFishing;
    }

    void Update()
    {
        if (item == null || !canFishing) return;

        if (inputManager.FishPressed())
        {
            Init(item);
            uiManager.SetUI(UIManager.state.Fishing);
            inputManager.EnableUIInput(true);
            vm.Start();
        }

        if (inputManager.CatchFish())
        {
            if (vm == null) return;
            bool result = timingBarView.IsSuccess();
            bool isFinish = vm.Handle(result);

            if (isFinish)
            {
                canFishing = false;
                InventoryEvent.OnAddItem?.Invoke(item);
            }
        }
    }

    void Init(ItemData item)
    {
        if (vm != null) vm = null;

        switch (item.minigameType)
        {
            case MinigameType.TimingBar:
                var temp = new TimingBarViewModel(item, CaculatorDificult(item));
                vm = temp;
                timingBarView.Bind(temp);
                break;
            case MinigameType.none:
                break;
        }
    }

    void Clear()
    {
        if (timingBarView != null)
            timingBarView.UnBind();
        vm = null;
    }

    void HandleEnableFishing(ItemData item)
    {
        this.item = item;
        canFishing = true;
    }

    void HandleUnableFishing()
    {
        item = null;
        canFishing = false;

        Clear();
    }

    float CaculatorDificult(ItemData item)
    {
        return Mathf.Clamp01(item.weight / item.MAX_WEIGHT * 0.3f + item.value / item.MAX_VALUE * 0.7f);
    }
}
