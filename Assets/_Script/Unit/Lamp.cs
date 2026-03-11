using System.Collections;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] Light lampLight;
    [SerializeField] private float normalIntensity = 7f;
    [SerializeField] private float flickerDuration = 0.5f;   // thời gian nhấp nháy
    [SerializeField] private float flickerSpeed = 0.05f;     // tốc độ nhấp nháy

    void Awake()
    {
        lampLight = GetComponent<Light>();

        TimeEvent.OnNightTime += TurnOnLamp;
        TimeEvent.OnDayTime += TurnOffLamp;
    }

    void Disable()
    {
        TimeEvent.OnNightTime -= TurnOnLamp;
        TimeEvent.OnDayTime -= TurnOffLamp;
    }

    private void TurnOffLamp()
    {
        if (lampLight == null) return;
        lampLight.enabled = false;
    }

    private void TurnOnLamp()
    {
        if (lampLight == null) return;
        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        lampLight.enabled = true;

        float timer = 0f;

        while (timer < flickerDuration)
        {
            lampLight.intensity = Random.Range(0.2f, normalIntensity);
            timer += flickerSpeed;
            yield return new WaitForSeconds(flickerSpeed);
        }

        // sau khi nhấp nháy xong → sáng bình thường
        lampLight.intensity = normalIntensity;
    }

}
