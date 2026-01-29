using UnityEngine;

public class FishZoneController : MonoBehaviour
{
    [Header("Zone Settings")]
    public bool isActive = true;
    public ParticleSystem zoneVisual;
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
        FishingEvent.OnEnableFishing?.Invoke(item);
        ditectedPlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isActive) return;
        if (!other.CompareTag("Player")) return;

        FishingEvent.OnUnableFishing?.Invoke();
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
