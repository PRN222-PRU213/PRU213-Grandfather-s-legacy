using UnityEngine;

public class FishZoneController : MonoBehaviour
{
    [SerializeField] private FishingSystem fishingSystem;

    [Header("Zone Settings")]
    public bool isActive = true;          // Zone có đang hoạt động không
    public ParticleSystem zoneVisual;         // Hiệu ứng vùng cá (vòng tròn nước, particle,...)
    public bool ditectedPlayer = false;
    public ItemData item;

    private void Awake()
    {
        SetZoneActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (!other.CompareTag("Player")) return;

        // Báo cho hệ thống câu cá
        fishingSystem.SetCanFish(true, item);
        ditectedPlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isActive) return;
        if (!other.CompareTag("Player")) return;

        fishingSystem.SetCanFish(false, null);
        ditectedPlayer = false;
    }

    // Có thể gọi từ spawner để bật tắt zone
    public void SetZoneActive(bool value)
    {
        isActive = value;
        if (value)
        {
            zoneVisual.Play();

        }
        else
        {
            zoneVisual.Stop();
        }
    }
}
