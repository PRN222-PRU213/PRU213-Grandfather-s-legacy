using UnityEngine;
using UnityEngine.UI;

public class FishingMinigame : MonoBehaviour
{
    public enum FishingState
    {
        Idle,
        Playing,
        CaughtFish,
        Failed
    }

    public GameObject fishingUI;

    [Header("References")]
    public RectTransform successZone;     // Tay quay
    public RectTransform trackZone;      // Vùng bắt cá
    public Image successImg;
    public Image processMaker;
    public Transform roundMarkerParent;
    public GameObject roundMarkerPrefab;

    [Header("GamePlay")]
    public float maxTrackSpeed = 300f;      // Tốc độ di chuyển thanh
    public float minTrackSpeed = 100f;
    private float trackSpeed;

    public int maxSuccessZoneSize = 30;   // Kích thước vùng bắt cá lớn nhất
    public int minSuccessZoneSize = 5;   // Kích thước vùng bắt
    private int successZoneSize;
    private int angle;
    public int maxRounds = 5;
    public int minRounds = 2;
    private int totalRounds;
    private int currentRound = 0;

    private FishingState state = FishingState.Idle;
    public System.Action OnFinish;

    public void RunMinigame()
    {
        currentRound = 0;
        trackZone.localEulerAngles = Vector3.zero;
        processMaker.fillAmount = 0f;
        totalRounds = Random.Range(minRounds, maxRounds);
        DeleteAllPrefabs(roundMarkerParent);
        AddPrefab(roundMarkerPrefab, roundMarkerParent, totalRounds);
        EachTurn();
        state = FishingState.Playing;
    }

    void Update()
    {
        if (state != FishingState.Playing)
            return;
        MoveTrack();
        CheckInput();
    }

    void DeleteAllPrefabs(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    void AddPrefab(GameObject prefab, Transform parent, int count)
    {
        for (int i = 0; i < count - 1; i++)
        {
            Instantiate(prefab, parent);
        }
    }

    void EachTurn()
    {
        trackSpeed = Random.Range(minTrackSpeed, maxTrackSpeed);
        successZoneSize = Random.Range(minSuccessZoneSize, maxSuccessZoneSize);
        angle = Random.Range(0, 360);
        successZone.localEulerAngles = new Vector3(0, 0, angle);
        successImg.fillAmount = successZoneSize / 100f;
    }

    void MoveTrack()
    {
        trackZone.Rotate(0, 0, -trackSpeed * Time.deltaTime);
    }

    void CheckInput()
    {
        if (state == FishingState.Playing && GameInput.Instance.CatchFish())
        {
            if (IsSuccess())
            {
                Debug.Log("🎉 Caught Fish!");
                currentRound++;
                processMaker.fillAmount = (float)currentRound / totalRounds;
            }
            else
            {
                Debug.Log("❌ Failed!");
            }

            if (currentRound < totalRounds)
            {
                EachTurn();
                trackZone.localEulerAngles = Vector3.zero;
                return;
            }

            OnFinish?.Invoke();
            OnFinish = null;
            fishingUI.SetActive(false);
            state = FishingState.Idle;
        }
    }

    bool IsSuccess()
    {
        float trackAngle = trackZone.localEulerAngles.z;
        float successBeginAngle = successZone.localEulerAngles.z;

        float zoneRange = successImg.fillAmount * 360f;
        float successEndAngle = CaculatorAngle(successBeginAngle, zoneRange);

        Debug.Log(successBeginAngle + " " + trackAngle + " " + successEndAngle);

        return IsAngleInRange(trackAngle, successEndAngle, successBeginAngle);
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
