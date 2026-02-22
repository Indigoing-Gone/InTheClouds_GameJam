using UnityEngine;

public class SkyGridCell
{
    private Vector2 cellCenter;
    private SkyGridCellVisual cellVisual;
    private bool cloudDetected;
    private bool cloudIntended;

    public Vector2 CellCenter { get => cellCenter; }
    public bool CloudDetected
    {
        get => cloudDetected;
        set
        {
            cloudDetected = value;
            cellVisual.UpdateOverlayVisual(cloudDetected);
        }
    }
    public bool CloudIntended
    {
        get => cloudIntended;
        set
        {
            cloudIntended = value;
            cellVisual.UpdatePatternVisual(cloudIntended);
        }
    }


    public SkyGridCell(Vector2 _cellCenter, SkyGridCellVisual _cellVisual, Vector2 _cellSize)
    {
        cellCenter = _cellCenter;
        cellVisual = _cellVisual;
        cloudDetected = false;
        cloudIntended = false;

        cellVisual.InitializeCell(cellCenter, _cellSize);
    } 
}
