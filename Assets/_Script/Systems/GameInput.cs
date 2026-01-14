using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : Singleton<GameInput>
{
    public InputActionAsset inputActions;

    private InputAction m_MoveAction;
    private InputAction m_FishAction;
    private InputAction m_CatchFishAction;
    private InputAction m_OpenInventoryAction;
    private InputAction m_CloseInventoryAction;

    private void OnEnablePlayer()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisablePlayer()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void OnEnableUI()
    {
        inputActions.FindActionMap("UI").Enable();
    }

    private void OnDisableUI()
    {
        inputActions.FindActionMap("UI").Disable();
    }

    public void EnableUIInput(bool enable)
    {
        Debug.Log("EnableUIInput: " + enable);
        if (enable)
        {
            OnEnableUI();
            OnDisablePlayer();
        }
        else
        {
            OnDisableUI();
            OnEnablePlayer();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        EnableUIInput(false);

        m_MoveAction = InputSystem.actions.FindAction("Move");
        m_FishAction = InputSystem.actions.FindAction("Fish");
        m_CatchFishAction = InputSystem.actions.FindAction("CatchFish");
        m_OpenInventoryAction = InputSystem.actions.FindAction("OpenInventory");
        m_CloseInventoryAction = InputSystem.actions.FindAction("CloseInventory");
    }

    void Update()
    {
        if (m_FishAction.WasPressedThisFrame()
        || m_OpenInventoryAction.WasPressedThisFrame())
        {
            Debug.Log("Fish pressed");
            EnableUIInput(true);
        }

        if (m_CloseInventoryAction.WasPressedThisFrame())
        {
            Debug.Log("Close inventory pressed");
            EnableUIInput(false);
        }

    }

    public Vector2 GetMovement()
    {
        return m_MoveAction.ReadValue<Vector2>();
    }


    public bool FishPressed()
    {
        return m_FishAction.WasPressedThisFrame();
    }

    public bool CatchFish()
    {
        return m_CatchFishAction.WasPressedThisFrame();
    }

    public bool OpenInventory()
    {
        return m_OpenInventoryAction.WasPressedThisFrame();
    }

    public bool CloseInventory()
    {
        return m_CloseInventoryAction.WasPressedThisFrame();
    }
}
