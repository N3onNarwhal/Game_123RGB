using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerRGBController : MonoBehaviour
{
    public ColorState currentState { get; private set; }
    public event System.Action<ColorState> OnColorChanged;
    private PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
        currentState = ColorState.Red;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.ColorSwitch.ColorChange.performed += OnColorChange;
    }

    private void OnDisable()
    {
        DisableColors();
    }

    public void DisableColors()
    {
        inputActions.ColorSwitch.ColorChange.performed -= OnColorChange;
        inputActions.Disable();
    }

    void OnColorChange(InputAction.CallbackContext ctx)
    {
        if (PauseManager.paused)
        {
            return;
        }

        int value = Mathf.RoundToInt(ctx.ReadValue<float>());
        ChangeState((ColorState)(value - 1));
    }

    void ChangeState(ColorState newState)
    {
        if (currentState == newState) { return; }

        // update state and invoke
        currentState = newState;
        OnColorChanged?.Invoke(newState);
        
    }

}
