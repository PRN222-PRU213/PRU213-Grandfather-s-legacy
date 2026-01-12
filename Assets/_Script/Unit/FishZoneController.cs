using UnityEngine;
using UnityEngine.Events;

public class FishZoneController : MonoBehaviour
{
    [Header("Zone Settings")]
    public bool isActive = true;          // Zone có đang hoạt động không
    public GameObject zoneVisual;         // Hiệu ứng vùng cá (vòng tròn nước, particle,...)

    [Header("Events")]
    public UnityEvent onPlayerEnterZone;
    public UnityEvent onPlayerExitZone;

    public bool ditectedPlayer = false;

    private void Start()
    {
        if (zoneVisual != null)
            zoneVisual.SetActive(isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (!other.CompareTag("Player")) return;

        // Báo cho hệ thống câu cá
        FishingInteraction.Instance.SetCanFish(true);
        ditectedPlayer = true;

        // Trigger hiệu ứng, âm thanh... nếu muốn
        onPlayerEnterZone?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isActive) return;
        if (!other.CompareTag("Player")) return;

        FishingInteraction.Instance.SetCanFish(false);
        ditectedPlayer = false;
        onPlayerExitZone?.Invoke();
    }

    // Có thể gọi từ spawner để bật tắt zone
    public void SetZoneActive(bool value)
    {
        isActive = value;

        if (zoneVisual != null)
            zoneVisual.SetActive(value);
    }
}
