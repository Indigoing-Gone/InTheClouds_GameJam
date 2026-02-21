using TMPro;
using UnityEngine;

public class RoundVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundCountText;

    public void UpdateRoundCount(int _currentRound, int _totalRounds)
    {
        roundCountText.text = $"{_currentRound}/{_totalRounds}";
    }
}
