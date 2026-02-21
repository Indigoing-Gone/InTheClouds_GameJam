using System;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private Draggable currentDraggable = null;
    [SerializeField] private LayerMask draggableLayers;
    private Vector3 dragOffset;

    void OnEnable()
    {
        playerInput.DragPressed += OnDragPressed;
    }

    void OnDisable()
    {
        playerInput.DragPressed -= OnDragPressed;
    }

    void Update()
    {
        if(currentDraggable == null) return;

        currentDraggable.transform.position = (Vector3)playerInput.DragPositionWorld + dragOffset;
    }

    private void OnDragPressed()
    {
        RaycastHit2D _hit = Physics2D.Raycast(playerInput.DragPositionWorld, Vector2.zero, 20.0f, draggableLayers);

        if(!_hit) return;
        _hit.transform.TryGetComponent<Draggable>(out Draggable _foundDraggable);
        if(_foundDraggable == null) return;

        currentDraggable = _foundDraggable;
        dragOffset = currentDraggable.transform.position - (Vector3)playerInput.DragPositionWorld;
        playerInput.DragReleased += OnDragReleased;
    }

    private void OnDragReleased()
    {
        currentDraggable = null;
        playerInput.DragReleased -= OnDragReleased;
    }
}
