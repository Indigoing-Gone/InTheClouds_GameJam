using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Input/PlayerInput")]
public class PlayerInput : ScriptableObject
{
    [SerializeField] private InputReader input;

    private Vector2 dragPositionWorld;
    private bool pressingDrag = false;

    public Vector2 DragPositionWorld { get => dragPositionWorld; private set { dragPositionWorld = value; } }
    public bool PressingDrag
    {
        get => pressingDrag;
        private set
        {
            pressingDrag = value;
            if(pressingDrag) DragPressed?.Invoke();
            else DragReleased?.Invoke();
        }
    }

    public event Action DragPressed;
    public event Action DragReleased;

    private void OnEnable()
    {
        input.PositionEvent += HandleDragPosition;
        input.DragEvent += HandlePressingDrag;
    }

    private void OnDisable()
    {
        input.PositionEvent -= HandleDragPosition;
        input.DragEvent -= HandlePressingDrag;
    }

    private void HandleDragPosition(Vector2 _position) => DragPositionWorld = _position;
    private void HandlePressingDrag(bool _state) => PressingDrag = _state;
}