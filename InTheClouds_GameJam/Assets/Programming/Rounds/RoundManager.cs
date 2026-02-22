using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RoundVisuals roundVisuals;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Sky Elements")]
    [SerializeField] private List<SkyPattern> skyPatterns;
    [SerializeField] private SkyGrid[] skyGrids;
    [SerializeField] private CloudSet[] cloudSets;
    [SerializeField] private List<Cloud> currentClouds;

    [Header("Round")]
    [SerializeField] private int totalRoundCount;
    private int currentRoundCount = 0;

    [SerializeField] private GameObject endPanel;

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
        if (currentRoundCount == totalRoundCount) endPanel.SetActive(true);
        else
        {
            currentRoundCount++;
            roundVisuals.UpdateRoundCount(currentRoundCount, totalRoundCount);
            UpdateSkyGrids();
            UpdateClouds();
        }
    }
    
    private void UpdateSkyGrids()
    {
        for (int i = 0; i < skyGrids.Length; i++)
        {
            int patternIndex = Random.Range(0, skyPatterns.Count);
            skyGrids[i].GetComponent<SkyGrid>().ValidationPattern = skyPatterns[patternIndex];
            skyPatterns.RemoveAt(patternIndex);
        }
    }

    private void UpdateClouds()
    {
        currentClouds.Clear();
        
        for (int i = 0; i < cloudSets.Length; i++)
        {
            for (int n = 0; n < cloudSets[i].AmountToSelect; n++)
            {
                int cloudIndex = Random.Range(0, cloudSets[i].clouds.Length);
                Cloud _newCloud = Instantiate(cloudSets[i].clouds[cloudIndex], Vector3.zero, Quaternion.identity).GetComponent<Cloud>();
                currentClouds.Add(_newCloud);
            }
        }
    }
}
