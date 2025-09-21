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
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.ColorSwitch.ColorChange.performed += OnColorChange;
    }


    void OnColorChange(InputAction.CallbackContext ctx)
    {
        int value = Mathf.RoundToInt(ctx.ReadValue<float>());
        ChangeState((ColorState)(value - 1));
    }

    void ChangeState(ColorState newState)
    {
        if (newState != currentState)
        {
            currentState = newState;
            OnColorChanged?.Invoke(newState);
        }
    }

}
