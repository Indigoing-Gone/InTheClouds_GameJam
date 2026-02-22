using System;
using System.Collections;
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
    private bool currentlyScoring;

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
        if(currentlyScoring) return;
        StartCoroutine(ScoreGridsCoroutine());
    }

    private IEnumerator ScoreGridsCoroutine()
    {
        currentlyScoring = true;

        int _correct, _incorrect, _accuracy, _addedScore;
        for (int i = 0; i < skyGrids.Length; i++)
        {
            (_correct, _incorrect) = skyGrids[i].ValidateSkyGrid();
            (_accuracy, _addedScore) = CalculatePoints(_correct, _incorrect);

            scoreVisuals.GenerateScorePopup(skyGrids[i].transform.position, $"{_accuracy}%");
            yield return new WaitForSeconds(0.5f);

            scoreVisuals.GenerateScorePopup(skyGrids[i].transform.position, $"+{_addedScore}");
            scoreVisuals.UpdateTotalScore(totalScore);

            yield return new WaitForSeconds(0.7f);
        }

        yield return new WaitForSeconds(1.0f);

        ScoringEnded?.Invoke();
        currentlyScoring = false;
    }

    private (int, int) CalculatePoints(float _correct, float _incorrect)
    {
        float _accuracy = _correct / (_correct + _incorrect);
        int _addedScore = Mathf.Max(0, Mathf.CeilToInt(scoreCurve.Evaluate(_accuracy)));
        totalScore += _addedScore;
        return (Mathf.CeilToInt(_accuracy * 100), _addedScore);
    }
}
