using System;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    public event Action NextRoundStarting;

    public void ConfirmButtonPressed()
    {
        NextRoundStarting?.Invoke();
    }
}
