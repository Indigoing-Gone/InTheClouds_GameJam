using TMPro;
using UnityEngine;

public class ScoreVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [SerializeField] private GameObject popupTextObject;

    public void UpdateTotalScore(int _totalScore)
    {
        totalScoreText.text = $"{_totalScore}";
        finalScoreText.text = $"{_totalScore}";
    }

    public void GenerateScorePopup(Vector3 _position, string _text)
    {
        PopupText _popupText = Instantiate(popupTextObject, _position, Quaternion.identity).GetComponent<PopupText>();
        _popupText.SetText($"+{_text}");
    }
}
