using System.Linq;
using UnityEngine;

public class SkyGrid : MonoBehaviour
{
    private class CellData
    {
        public Vector2 cellCenter;
        public bool cloudDetected;
        public bool cloudIntended;
    }

    [Header("Grid Parameters")]
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 cellSize;
    private CellData[,] skyGrid;

    [Header("Validation Parameters")]
    [SerializeField] private SkyPattern validationPattern;
    public SkyPattern ValidationPattern
    {
        get => validationPattern;
        set
        {
            validationPattern = value;
            UpdateIntendedCloudsSkyGrid();
        }
    }

    private void Awake()
    {
        GenerateSkyGrid();
        UpdateIntendedCloudsSkyGrid();
    }

    private void Update()
    {
        DetectCloudsOnSkyGrid();
    }

    private void GenerateSkyGrid()
    {
        skyGrid = new CellData[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                skyGrid[x, y] = new CellData
                {
                    cellCenter = transform.position + GetScaledGridPosition(x, y),
                    cloudDetected = false,
                    cloudIntended = false,
                };
            }
        }
    }

    private void DetectCloudsOnSkyGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                RaycastHit2D _hit = Physics2D.Raycast(skyGrid[x, y].cellCenter, Vector2.zero, 20.0f);

                if(!_hit)
                {
                    skyGrid[x, y].cloudDetected = false;
                    continue;
                }

                _hit.transform.TryGetComponent<Cloud>(out Cloud _foundCloud);
                if(_foundCloud == null) return;

                skyGrid[x, y].cloudDetected = true;
            }
        }
    }

    private void UpdateIntendedCloudsSkyGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
            for (int y = 0; y < gridSize.y; y++)
                skyGrid[x, y].cloudIntended = false;

        if(ValidationPattern == null) return;

        for (int i = 0; i < ValidationPattern.IntendedCloudPositions.Count; i++)
        {
            Vector2Int _cloudPosition = ValidationPattern.IntendedCloudPositions[i];
            skyGrid[_cloudPosition.x, _cloudPosition.y].cloudIntended = true;
        }
    }

    public (int, int) ValidateSkyGrid()
    {
        int correct = 0, incorrect = 0;
        
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if(!skyGrid[x, y].cloudDetected && !skyGrid[x, y].cloudIntended) continue;
                else if(skyGrid[x, y].cloudDetected == skyGrid[x, y].cloudIntended) correct++;
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

                // Vector3 _end = transform.position + GetScaledGridPosition(x + 1, y);
                // Gizmos.DrawLine(_start, _end);

                // _end = transform.position + GetScaledGridPosition(x, y + 1);
                // Gizmos.DrawLine(_start, _end);

                if(Application.isPlaying)
                {
                    Gizmos.color = 
                        skyGrid[x, y].cloudDetected 
                        ? Color.green : 
                        skyGrid[x, y].cloudIntended
                        ? Color.blue
                        : Color.black;
                }
                Gizmos.DrawSphere(_start, cellSize.x / 2);
            }
        }
    }
}
