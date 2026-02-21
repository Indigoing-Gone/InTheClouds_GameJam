using UnityEngine;

[RequireComponent(typeof(ScoreVisuals))]
public class ScoreManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ScoreVisuals visuals;
    [SerializeField] private SkyGrid[] skyGrids;
    [SerializeField] private ConfirmButton confirmButton;

    [Header("Scoring")]
    [SerializeField] private int totalScore;
    [SerializeField] private int correctPoints;
    [SerializeField] private int incorrectPoints;

    private void OnEnable()
    {
        //confirmButton.NextRoundStarting += ScoreGrids;
    }

    private void OnDisable()
    {
        //confirmButton.NextRoundStarting -= ScoreGrids;
    }

    void Awake()
    {
        visuals = GetComponent<ScoreVisuals>();
    }

    private void ScoreGrids()
    {
        int _correct, _incorrect;
        for (int i = 0; i < skyGrids.Length; i++)
        {
            (_correct, _incorrect) = skyGrids[i].ValidateSkyGrid();
            CalculatePoints(_correct, _incorrect);
            visuals.UpdateTotalScore(totalScore);
        }
    }

    private void CalculatePoints(int _correct, int _incorrect)
    {
        int _calculatedScore = (_correct * correctPoints) - (_incorrect * incorrectPoints);
        totalScore += Mathf.Max(0, _calculatedScore);
    }
}
