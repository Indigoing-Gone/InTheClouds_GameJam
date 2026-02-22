using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private RoundManager RoundManager;
    public event Action ScoringStarted;
    
    public void BeginGame()
    {
        startPanel.SetActive(false);
        RoundManager.RoundEnd();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ConfirmRound()
    {
        ScoringStarted?.Invoke();
    }
}
