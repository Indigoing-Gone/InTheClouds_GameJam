using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

[CreateAssetMenu(menuName = "Input/InputReader")]
[DefaultExecutionOrder(-1)]
public class InputReader : ScriptableObject, IPlayerActions
{
    private PlayerInputActions playerInput;

    #region Callbacks

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInputActions();
            playerInput.Player.SetCallbacks(this);

            DisableAll();
        }
    }

    public void SetGameplay()
    {
        playerInput.Player.Enable();
    }
    public void DisableAll()
    {
        playerInput.Player.Disable();
    }

    #endregion

    #region Events

    //Gameplau
    public event Action<Vector2> PositionEvent;
    public event Action<bool> DragEvent;

    #endregion

    #region Triggers

    //Mouse Position
    public void OnPosition(InputAction.CallbackContext context)
    {
        PositionEvent?.Invoke(Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()));
    }

    //Drag
    public void OnDrag(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) DragEvent?.Invoke(true);
        if (context.phase == InputActionPhase.Canceled) DragEvent?.Invoke(false);
    }

    #endregion
}