using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RoundVisuals roundVisuals;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Sky Elements")]
    [SerializeField] private List<SkyPattern> skyPatterns;
    [SerializeField] private SkyGrid[] skyGrids;

    [Header("Round")]
    [SerializeField] private int totalRoundCount;
    private int currentRoundCount = 0;

    void OnEnable()
    {
        scoreManager.ScoringEnded += RoundEnd;
    }

    void OnDisable()
    {
        scoreManager.ScoringEnded -= RoundEnd;
    }

    void Awake()
    {
        roundVisuals = GetComponent<RoundVisuals>();
        roundVisuals.UpdateRoundCount(currentRoundCount, totalRoundCount);
    }

    public void RoundEnd()
    {
        currentRoundCount++;
        roundVisuals.UpdateRoundCount(currentRoundCount, totalRoundCount);
        UpdateGrids();
    }
    
    private void UpdateGrids()
    {
        for (int i=0; i < skyGrids.Length; i++)
        {
            int patternIndex = Random.Range(0, skyPatterns.Count);
            skyGrids[i].GetComponent<SkyGrid>().ValidationPattern = skyPatterns[patternIndex];
            skyPatterns.RemoveAt(patternIndex);
        }
    }
}
