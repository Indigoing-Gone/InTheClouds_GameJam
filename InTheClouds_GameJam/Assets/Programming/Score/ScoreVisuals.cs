using TMPro;
using UnityEngine;

public class ScoreVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    public void UpdateTotalScore(int _totalScore)
    {
        totalScoreText.text = $"{_totalScore}";
        finalScoreText.text = $"{_totalScore}";
    }
}
