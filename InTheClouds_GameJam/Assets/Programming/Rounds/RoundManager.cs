using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UIElements;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private List<SkyPattern> SkyPatterns;
    [SerializeField] SkyGrid[] SkyGrids;
    public int roundCount;
    

    void Start()
    {
        //updateGrids();
        roundCount = 1;
    }

    public void roundEnd()
    {
        roundCount++;
        
        updateGrids();
    }
    
    private void updateGrids()
    {
        for (int i=0; i < SkyGrids.Length; i++)
        {
            int patternIndex = Random.Range(0, SkyPatterns.Count);
            SkyGrids[i].GetComponent<SkyGrid>().ValidationKey = SkyPatterns[patternIndex];
            SkyPatterns.RemoveAt(patternIndex);
        }
    }
}
