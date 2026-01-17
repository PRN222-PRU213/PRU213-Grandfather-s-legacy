using Unity.Cinemachine;
using UnityEngine;

public class FreeLookInputSystem : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CinemachineInputAxisController freeLook;

    void Update()
    {
        bool rotating = inputManager.GetCameraRotation();

        HandleCursor(rotating);

        freeLook.enabled = rotating;

    }

    void HandleCursor(bool rotating)
    {
        if (rotating)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
