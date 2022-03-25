using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridMap : MonoBehaviour, IGridProvider
{
    public float Height => transform.position.y;
    
    [SerializeField] [Min(0)] private Vector2Int mapSize;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private TileGenerator tileGenerator;
    [SerializeField] private ResourcesGenerator resourcesGenerator;

    [Header("Gizmos")] 
    [SerializeField] private bool drawLabels;
    [SerializeField] private bool drawGeneratedMap;

    private Cell[,] cells;

    public void Init()
    {
        cells = new Cell[mapSize.y, mapSize.x];

        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                Vector2Int cellMapPos = new Vector2Int(i, j);
                Vector3 cellWorldPos = GetCellWorldPos(cellMapPos);
                Cell cell = new Cell(cellMapPos, cellWorldPos);

                if (tileGenerator.TryGetTerrainPrefab(cellMapPos, mapSize, out GameObject objToSpawn))
                {
                    GameObject objSpawned = Instantiate(objToSpawn, transform);
                    objSpawned.transform.position = cell.WorldPos;
                    cell.SetPlacementActive(true);
                }
                else
                {
                    cell.SetPlacementActive(false);
                }
                
                cells[i, j] = cell;
            }
        }
        
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                Cell cell = cells[i, j];
                if (!cell.IsCellPlaceable) continue;
                
                GameObject resourcePrefab = resourcesGenerator.GetResourcePrefab(cell.MapPos, mapSize, out Color color);

                //print(resourcePrefab);
                if (resourcePrefab != null)
                {
                    GameObject resInstance = Instantiate(resourcePrefab, transform);
                    resInstance.transform.position = cell.WorldPos;
                }
            }
        }
    }

    public Vector3 GetSnappedPosition(Vector3 worldPos)
    {
        Vector2Int mapPos = GetMapFromWorldPos(worldPos);
        return GetCellWorldPos(mapPos);
    }

    public CellData GetCellData(Vector3 worldPos)
    {
        Vector2Int mapPos = GetMapFromWorldPos(worldPos);
        return cells[mapPos.x, mapPos.y].GetCellData();
    }

    public bool IsCellNotPlaceable(Vector3 worldPos)
    {
        Vector2Int mapPos = GetMapFromWorldPos(worldPos);
        Cell cell = cells[mapPos.x, mapPos.y];

        return cell.IsOccupied || !cell.IsGround;
    }

    public bool TryPlaceGridObject(Vector2Int mapPos, GridObject gridObject, Quaternion rotation)
    {
        Cell cell;
        
        try
        {
            cell = cells[mapPos.x, mapPos.y];
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError($"Cell with index {mapPos} does not exist");
            return false;
        }

        if (cell.IsOccupied || !cell.IsGround)
        {
            print("Cell is occupied");
            return false;
        }

        GridObject obj = Instantiate(gridObject, transform.position, rotation);
        cell.SetObject(obj);
        obj.SetParent(transform);
        obj.SetPosition(cell.WorldPos);

        return true;
    }

    private Vector2Int GetMapFromWorldPos(Vector3 worldPos)
    {
        Vector2Int mapPos = new Vector2Int((int) (worldPos.z / cellSize), 
                                           (int) (worldPos.x / cellSize));
        mapPos = new Vector2Int(Mathf.Clamp(mapPos.x, 0, mapSize.x - 1),
                                Mathf.Clamp(mapPos.y, 0, mapSize.y - 1));
        
        return mapPos;
    }

    private Vector3 GetCellWorldPos(Vector2Int mapPos)
    {
        Vector3 pos = transform.forward * (cellSize * (mapPos.x + 0.5f)) +
                      transform.right * (cellSize * (mapPos.y + 0.5f)) +
                      transform.position;
        return pos;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        /*Vector3 center = transform.position                              +
                         transform.forward * mapSize.x * 0.5f * cellSize +
                         transform.right   * mapSize.y * 0.5f * cellSize;

        float radius = mapSize.x * 0.5f * cellSize;*/
        
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                Vector2Int cellMapPos = new Vector2Int(i, j);
                Vector3 cellWorldPos = GetCellWorldPos(cellMapPos);
                //float distanceToCenter = Vector3.Distance(cellWorldPos, center) / radius;
                
                if (drawGeneratedMap)
                {
                    //float noise = Mathf.PerlinNoise(i * noiseCoefficient, j * noiseCoefficient);
                    /*if (noise > noiseClip && distanceToCenter < distanceClip)
                    {*/
                    resourcesGenerator.GetResourcePrefab(cellMapPos, mapSize, out Color color);
                    if (color != Color.black) Gizmos.color = color;
                    else
                    {
                        Gizmos.color = tileGenerator.GetTerrainGizmosColor(cellMapPos, mapSize);
                    }
                    
                    Gizmos.DrawCube(cellWorldPos,new Vector3(cellSize, 0, cellSize));
                }
                else
                {
                    Gizmos.DrawWireCube(cellWorldPos, new Vector3(cellSize, 0, cellSize));
                }

                if (drawLabels)
                {
                    Handles.Label(cellWorldPos, cellMapPos.ToString());
                }
            }
        }
    }
    
#endif
    
}
