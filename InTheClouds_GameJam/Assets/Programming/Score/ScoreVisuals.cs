using TMPro;
using UnityEngine;

public class ScoreVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalScoreText;

    private void Awake()
    {
        totalScoreText.text = "0";
    }

    public void UpdateTotalScore(int _totalScore)
    {
        totalScoreText.text = $"{_totalScore}";
    }
}
