using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("Cinemachine")]
    [SerializeField] private CinemachineCamera normalCamera;
    [SerializeField] private CinemachineCamera fishingCamera;
    [SerializeField] private CinemachineCamera defaultCamera;

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
        defaultCamera.Priority = 0;
    }

    public void ExitFishingView()
    {
        SetNormalView();
    }

    // ================= INTERNAL =================

    void SetNormalView()
    {
        defaultCamera.Priority = 20;
        fishingCamera.Priority = 0;
    }
}