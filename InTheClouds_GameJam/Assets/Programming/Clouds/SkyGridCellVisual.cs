using UnityEngine;

public class SkyGridCellVisual : MonoBehaviour
{
    public void InitializeCell(Vector3 _position, Vector3 _scale)
    {
        transform.position = _position;
        transform.localScale = _scale;
    }
}
