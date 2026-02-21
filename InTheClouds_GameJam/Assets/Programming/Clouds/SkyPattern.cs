using UnityEngine;

[CreateAssetMenu(menuName = "Cloud/Sky Pattern")]
public class SkyPattern : ScriptableObject
{
    public string cloudPatternName;
    public Vector2Int[] IntendedCloudPositions;
}
