using UnityEngine;
using UnityEngine.InputSystem;

public class FishingInteraction : Singleton<FishingInteraction>
{
    [SerializeField] private FishingMinigame fishingMinigame;

    [Header("Fishing Settings")]
    public bool canFish = false;          // Đang đứng trong Fish Zone?
    public bool isFishing = false;        // Đang mở UI mini game chưa?

    [Header("UI Mini Game")]
    public GameObject fishingUI;          // Gắn panel UI mini game vào đây

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        // Nếu không ở trong zone → không làm gì
        if (!canFish) return;

        // Nếu đang câu rồi thì không cho bấm nữa
        if (isFishing) return;

        // Nhấn F để bắt đầu
        if (GameInput.Instance.FishPressed())
        {
            StartFishing();
        }
    }

    public void StartFishing()
    {
        isFishing = true;

        // Mở UI
        if (fishingUI != null)
            fishingUI.SetActive(true);

        // (Tuỳ chọn) Khoá điều khiển Player
        GameInput.Instance.EnablePlayerInput(false);
        GameInput.Instance.EnableUIInput(true);
        CameraController.Instance.EnterFishingView();
        fishingMinigame.OnFinish += EndFishing;
        Debug.Log("Fishing Mini Game Started!");

        fishingMinigame.RunMinigame();
    }

    // Hàm này gọi khi mini game kết thúc
    public void EndFishing()
    {
        isFishing = false;

        if (fishingUI != null)
            fishingUI.SetActive(false);

        // Mở lại điều khiển player nếu bạn từng khoá
        GameInput.Instance.EnablePlayerInput(true);
        GameInput.Instance.EnableUIInput(false);

        CameraController.Instance.ExitFishingView();
        Debug.Log("Fishing Ended");
    }

    // Được gọi từ FishZoneController
    public void SetCanFish(bool value)
    {
        canFish = value;

        // Nếu rời khỏi zone mà đang câu → tắt luôn
        if (!value && isFishing)
            EndFishing();
    }
}
