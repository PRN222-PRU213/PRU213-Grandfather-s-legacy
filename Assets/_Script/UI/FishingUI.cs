using UnityEngine;
using UnityEngine.UI;

public class FishingUI : MonoBehaviour
{
    [Header("References")]
    public RectTransform successZone;     // Tay quay
    public RectTransform trackZone;      // Vùng bắt cá
    public Image successImg;
    public Image itemImg;
    public Image processMaker;
    public Transform roundMarkerParent;
    public GameObject roundMarkerPrefab;

    private int angle;

    public void Setup(ItemData item, int totalRounds, float difficulty)
    {
        trackZone.localEulerAngles = Vector3.zero;
        processMaker.fillAmount = 0f;
        itemImg.sprite = item.icon;
        float hue = Mathf.Lerp(0.33f, 0f, difficulty);
        itemImg.color = Color.HSVToRGB(hue, 0.9f, 1f);

        DeleteMaker(roundMarkerParent);
        AddMarker(roundMarkerPrefab, roundMarkerParent, totalRounds);
    }

    void DeleteMaker(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    void AddMarker(GameObject prefab, Transform parent, int count)
    {
        for (int i = 0; i < count - 1; i++)
        {
            Instantiate(prefab, parent);
        }
    }

    public void EachTurn(float successZoneSize)
    {
        angle = Random.Range(0, 360);
        successZone.localEulerAngles = new Vector3(0, 0, angle);
        successImg.fillAmount = successZoneSize / 100f;
    }

    public void MoveTrack(float trackSpeed)
    {
        trackZone.Rotate(0, 0, -trackSpeed * Time.deltaTime);
    }

    public void IncreaseProcess(float currentRound, float totalRounds)
    {
        processMaker.fillAmount = (float)currentRound / totalRounds;
    }

    // void CheckCatch()
    // {
    //     if (state == FishingState.Playing && InputManager.Instance.CatchFish())
    //     {
    //         if (IsSuccess())
    //         {
    //             currentRound++;
    //             processMaker.fillAmount = (float)currentRound / totalRounds;
    //         }

    //         if (currentRound < totalRounds)
    //         {
    //             EachTurn();
    //             return;
    //         }

    //         OnFinish?.Invoke();
    //         OnFinish = null;
    //         state = FishingState.Idle;
    //         system.AddItem(item);
    //     }
    // }

    public bool IsSuccess()
    {
        float trackAngle = trackZone.localEulerAngles.z;
        float successBeginAngle = successZone.localEulerAngles.z;

        float zoneRange = successImg.fillAmount * 360f;
        float successEndAngle = CaculatorAngle(successBeginAngle, zoneRange);

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
