using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cloud/Sky Pattern")]
public class SkyPattern : ScriptableObject
{
    public string cloudPatternName;
    [TextArea(4,10)] public string cloudPattern;
    public List<Vector2Int> IntendedCloudPositions;

    private void OnValidate()
    {
        IntendedCloudPositions.Clear();
        string[] _pattern = cloudPattern.Split("\n");

        for(int y = _pattern.Length - 1; y >= 0; y--)
            for(int x = 0; x < _pattern[y].Length; x++)
                if(_pattern[y][x] == 'X') IntendedCloudPositions.Add(new(x,y));
    }
}