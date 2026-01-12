using UnityEngine;
using Unity.Cinemachine;

public class CameraController : Singleton<CameraController>
{
    [Header("Cinemachine")]
    [SerializeField] private CinemachineCamera normalCamera;
    [SerializeField] private CinemachineCamera fishingCamera;

    private CinemachineBrain brain;

    protected override void Awake()
    {
        base.Awake();
        brain = Camera.main.GetComponent<CinemachineBrain>();

        SetNormalView();
    }

    // ================= PUBLIC API =================

    public void EnterFishingView()
    {
        fishingCamera.Priority = 20;
        normalCamera.Priority = 0;
    }

    public void ExitFishingView()
    {
        SetNormalView();
    }

    // ================= INTERNAL =================

    void SetNormalView()
    {
        normalCamera.Priority = 20;
        fishingCamera.Priority = 0;
    }
}