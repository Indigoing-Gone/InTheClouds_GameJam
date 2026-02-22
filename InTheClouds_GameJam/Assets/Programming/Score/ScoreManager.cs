using System;
using UnityEngine;

[RequireComponent(typeof(ScoreVisuals))]
public class ScoreManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ScoreVisuals scoreVisuals;
    [SerializeField] private SkyGrid[] skyGrids;
    [SerializeField] private Buttons buttons;

    [Header("Scoring")]
    [SerializeField] private int totalScore;
    [SerializeField] private AnimationCurve scoreCurve;

    public event Action ScoringEnded;


    private void OnEnable()
    {
        buttons.ScoringStarted += ScoreGrids;
    }

    private void OnDisable()
    {
        buttons.ScoringStarted -= ScoreGrids;
    }

    void Awake()
    {
        scoreVisuals = GetComponent<ScoreVisuals>();
        scoreVisuals.UpdateTotalScore(totalScore);
    }

    private void ScoreGrids()
    {
        int _correct, _incorrect;
        for (int i = 0; i < skyGrids.Length; i++)
        {
            (_correct, _incorrect) = skyGrids[i].ValidateSkyGrid();
            CalculatePoints(_correct, _incorrect);
            scoreVisuals.UpdateTotalScore(totalScore);
        }

        ScoringEnded?.Invoke();
    }

    private void CalculatePoints(float _correct, float _incorrect)
    {
        float _accuracy = _correct / (_correct + _incorrect);
        totalScore += Mathf.Max(0, Mathf.CeilToInt(scoreCurve.Evaluate(_accuracy)));
    }
}
