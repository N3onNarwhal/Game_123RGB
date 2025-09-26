using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerRGBController : MonoBehaviour
{
    public ColorState currentState;
    public event System.Action<ColorState> OnColorChanged;
    private PlayerControls inputActions;
    public PlayerInteraction playerInteraction;

    private void Awake()
    {
        inputActions = new PlayerControls();
        //currentState = ColorState.Red;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.ColorSwitch.ColorChange.performed += OnColorChange;
        inputActions.ColorSwitch.ColorScroll.performed += ScrollColorChange;
    }

    private void OnDisable()
    {
        inputActions.ColorSwitch.ColorChange.performed -= OnColorChange;
        inputActions.ColorSwitch.ColorScroll.performed -= ScrollColorChange;
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
        playerInteraction.OnPlayerColorChange();
    }

    void ChangeState(ColorState newState)
    {
        if (currentState == newState) { return; }

        // update state and invoke
        currentState = newState;
        OnColorChanged?.Invoke(newState);
        
    }

    void ScrollColorChange(InputAction.CallbackContext ctx)
    {
        if (PauseManager.paused)
        {
            return;
        }
        
        Vector2 scrollDelta = ctx.ReadValue<Vector2>();

        // if scroll up
        if (scrollDelta.y > 0)
        {
            // if at blue, become red
            if (currentState == ColorState.Blue)
            {
                ChangeState(ColorState.Red);
            }
            // otherwise, just cycle
            else
            {
                ChangeState(currentState + 1);
            }

            playerInteraction.OnPlayerColorChange();

        }
        else if (scrollDelta.y < 0)
        {
            // if at red, become blue
            if (currentState == ColorState.Red)
            {
                ChangeState(ColorState.Blue);
            }
            // otherwise, just cycle
            else
            {
                ChangeState(currentState - 1);
            }

            playerInteraction.OnPlayerColorChange();

        }
    }

}
