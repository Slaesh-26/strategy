using System;
using System.Collections.Generic;
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

    private GridCell[,] cells;

    public void Init()
    {
        cells = new GridCell[mapSize.y, mapSize.x];

        // spawning tiles
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                Vector2Int cellMapPos = new Vector2Int(i, j);
                Vector3 cellWorldPos = GetCellWorldPos(cellMapPos);
                GridCell gridCell = new GridCell(cellMapPos, cellWorldPos);

                if (tileGenerator.TryGetTerrainPrefab(cellMapPos, mapSize, out GameObject objToSpawn))
                {
                    GameObject objSpawned = Instantiate(objToSpawn, transform);
                    objSpawned.transform.position = gridCell.WorldPos;
                    gridCell.SetGround(true);
                }
                else
                {
                    gridCell.SetGround(false);
                }
                
                cells[i, j] = gridCell;
            }
        }
        
        // spawning resources
        /*for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                GridCell gridCell = cells[i, j];
                if (!gridCell.IsPlaceable) continue;
                
                Resource resource = resourcesGenerator.CreateResource(gridCell.MapPos, gridCell.WorldPos, mapSize);

                if (resource != null)
                {
                    gridCell.AddObject(resource);
                }
            }
        }*/
        
        
    }

    public Vector3 GetSnappedPosition(Vector3 worldPos)
    {
        Vector2Int mapPos = GetMapFromWorldPos(worldPos);
        return GetCellWorldPos(mapPos);
    }

    public bool CellContainsGridObject<T>(Vector2Int mapPos, out T gridObject) where T : GridObject
    {
        GridCell gridCell;
        
        // todo: make "TryGetCell" method
        try
        {
            gridCell = cells[mapPos.x, mapPos.y];
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError($"Cell with index {mapPos} does not exist");
            gridObject = null;
            
            return false;
        }

        bool contains = gridCell.ContainsGridObject<T>(out gridObject);
        return contains;
    }

    public CellData GetCellData(Vector3 worldPos)
    {
        Vector2Int mapPos = GetMapFromWorldPos(worldPos);
        return cells[mapPos.x, mapPos.y].GetCellData();
    }

    public List<GridObject> GetCellGridObjects(Vector2Int mapPos)
    {
        GridCell gridCell = cells[mapPos.x, mapPos.y];
        return gridCell.GetCellGridObjects();
    }

    public bool IsCellNotPlaceable(Vector3 worldPos)
    {
        Vector2Int mapPos = GetMapFromWorldPos(worldPos);
        GridCell gridCell = cells[mapPos.x, mapPos.y];

        return gridCell.IsOccupied || !gridCell.IsGround;
    }

    public bool TryPlaceGridObject(Vector2Int mapPos, GridObject gridObject)
    {
        GridCell gridCell;
        
        try
        {
            gridCell = cells[mapPos.x, mapPos.y];
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError($"Cell with index {mapPos} does not exist");
            return false;
        }

        if (gridCell.IsOccupied || !gridCell.IsGround)
        {
            Debug.LogError($"Cell {mapPos} is occupied");
            return false;
        }

        gridCell.AddObject(gridObject);
        gridObject.SetParent(transform);
        gridObject.SetPosition(gridCell.WorldPos);

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
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                Vector2Int cellMapPos = new Vector2Int(i, j);
                Vector3 cellWorldPos = GetCellWorldPos(cellMapPos);
                
                if (drawGeneratedMap)
                {
                    Color color = resourcesGenerator.GetGizmosColor(cellMapPos, cellWorldPos, mapSize);

                    if (color != Color.black)
                    {
                        Gizmos.color = color;
                    }
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
