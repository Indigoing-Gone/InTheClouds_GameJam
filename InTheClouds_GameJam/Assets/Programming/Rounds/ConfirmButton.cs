using System;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    public event Action ScoringStarted;

    public void ConfirmButtonPressed()
    {
        ScoringStarted?.Invoke();
    }
}
