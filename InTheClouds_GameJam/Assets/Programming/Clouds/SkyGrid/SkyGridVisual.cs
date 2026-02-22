using TMPro;
using UnityEngine;

public class SkyGridVisual : MonoBehaviour
{
    [SerializeField] private string patternTextHead;
    [SerializeField] private TextMeshProUGUI patternText;

    public void UpdatePatternText(string _text)
    {
        patternText.text = $"{patternTextHead} \n {_text}";
    }
}
