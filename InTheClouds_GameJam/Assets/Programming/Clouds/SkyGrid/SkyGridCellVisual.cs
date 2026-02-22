using UnityEngine;

public class SkyGridCellVisual : MonoBehaviour
{
    [SerializeField] private GameObject patternVisual;
    [SerializeField] private GameObject overlayVisual;

    public void InitializeCell(Vector3 _position, Vector3 _scale)
    {
        transform.position = _position;
        transform.localScale = _scale;
    }

    public void UpdatePatternVisual(bool _state)
    {
        patternVisual.SetActive(_state);
    }

    public void UpdateOverlayVisual(bool _state)
    {
        overlayVisual.SetActive(_state);
    }
}
