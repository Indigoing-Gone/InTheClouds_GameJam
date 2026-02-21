using TMPro;
using UnityEngine;

public class ScoreVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalScoreText;

    public void UpdateTotalScore(int _totalScore)
    {
        totalScoreText.text = $"{_totalScore}";
    }
}
