using UnityEngine;

public class SkyGrid : MonoBehaviour
{
    [Header("Components")]
    private SkyGridVisual skyGridVisual;

    [Header("Grid Parameters")]
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 cellSize;
    [SerializeField] private GameObject cellVisualObject;
    private SkyGridCell[,] skyGrid;

    [Header("Validation Parameters")]
    [SerializeField] private SkyPattern validationPattern;
    public SkyPattern ValidationPattern
    {
        get => validationPattern;
        set
        {
            validationPattern = value;

            if (ValidationPattern == null) return;

            UpdateIntendedCloudsSkyGrid();
            skyGridVisual.UpdatePatternText(validationPattern.cloudPatternName);
        }
    }

    private void Awake()
    {
        skyGridVisual = GetComponent<SkyGridVisual>();

        GenerateSkyGrid();
        UpdateIntendedCloudsSkyGrid();
    }

    private void Update()
    {
        DetectCloudsOnSkyGrid();
    }

    private void GenerateSkyGrid()
    {
        skyGrid = new SkyGridCell[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                SkyGridCellVisual _cellVisual = Instantiate(cellVisualObject, transform).GetComponent<SkyGridCellVisual>();
                skyGrid[x, y] = new SkyGridCell(
                    transform.position + GetScaledGridPosition(x, y),
                    _cellVisual,
                    cellSize
                );
            }
        }
    }

    private void DetectCloudsOnSkyGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                RaycastHit2D _hit = Physics2D.Raycast(skyGrid[x, y].CellCenter, Vector2.zero, 20.0f);

                if(!_hit)
                {
                    skyGrid[x, y].CloudDetected = false;
                    continue;
                }

                _hit.transform.TryGetComponent<Cloud>(out Cloud _foundCloud);
                if(_foundCloud == null) return;

                skyGrid[x, y].CloudDetected = true;
            }
        }
    }

    private void UpdateIntendedCloudsSkyGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
            for (int y = 0; y < gridSize.y; y++)
                skyGrid[x, y].CloudIntended = false;

        for (int i = 0; i < ValidationPattern.IntendedCloudPositions.Count; i++)
        {
            
            Vector2Int _cloudPosition = ValidationPattern.IntendedCloudPositions[i];
            skyGrid[_cloudPosition.x, _cloudPosition.y].CloudIntended = true;
        }
    }

    public (int, int) ValidateSkyGrid()
    {
        int correct = 0, incorrect = 0;
        
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if(!skyGrid[x, y].CloudDetected && !skyGrid[x, y].CloudIntended) continue;
                else if(skyGrid[x, y].CloudDetected == skyGrid[x, y].CloudIntended) correct++;
                else incorrect++;
            }
        }

        return (correct, incorrect);     
    }

    private Vector3 GetScaledGridPosition(int _gridX, int _gridY)
    {
        return Vector3.Scale(new(_gridX, _gridY), cellSize);
    }

    
    private void OnDrawGizmos()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Gizmos.color = Color.black;
                Vector3 _start = transform.position + GetScaledGridPosition(x, y);

                /*
                Vector3 _end = transform.position + GetScaledGridPosition(x + 1, y);
                Gizmos.DrawLine(_start, _end);

                _end = transform.position + GetScaledGridPosition(x, y + 1);
                Gizmos.DrawLine(_start, _end);

                if(Application.isPlaying)
                {
                    Gizmos.color = 
                        skyGrid[x, y].CloudDetected 
                        ? Color.green : 
                        skyGrid[x, y].CloudIntended
                        ? Color.blue
                        : Color.black;
                }
                */

                Gizmos.DrawSphere(_start, cellSize.x / 5);
            }
        }
    }
    
}