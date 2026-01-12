using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : Singleton<GameInput>
{
    public InputActionAsset inputActions;

    private InputAction m_MoveAction;
    private InputAction m_FishAction;
    private InputAction m_CatchFishAction;

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

    public void EnablePlayerInput(bool enabled)
    {
        if (enabled)
            OnEnablePlayer();
        else
            OnDisablePlayer();
    }

    public void EnableUIInput(bool enabled)
    {
        if (enabled)
            OnEnableUI();
        else
            OnDisableUI();
    }

    protected override void Awake()
    {
        base.Awake();
        EnableUIInput(false);

        m_MoveAction = InputSystem.actions.FindAction("Move");
        m_FishAction = InputSystem.actions.FindAction("Fish");
        m_CatchFishAction = InputSystem.actions.FindAction("CatchFish");
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
}
