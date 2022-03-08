using System;
using UnityEditor;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    [SerializeField] [Min(0)] private Vector2Int mapSize;
    [SerializeField] private float cellSize;

    [Header("Gizmos")] 
    [SerializeField] private bool drawLabels;

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
                cells[i, j] = cell;
            }
        }
    }

    public Vector3 GetSnappedPosition(Vector3 freePos)
    {
        Vector2Int mapPos = new Vector2Int((int) (freePos.z / cellSize), (int) (freePos.x / cellSize));
        mapPos = new Vector2Int(Mathf.Clamp(mapPos.x, 0, mapSize.x - 1),
                                Mathf.Clamp(mapPos.y, 0, mapSize.y - 1));
        return GetCellWorldPos(mapPos);
    }

    private Vector3 GetCellWorldPos(Vector2Int mapPos)
    {
        Vector3 pos = transform.forward * (cellSize * (mapPos.x + 0.5f)) +
                      transform.right * (cellSize * (mapPos.y + 0.5f));
        return pos;
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        Init();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                Gizmos.DrawWireCube(cells[i,j].WorldPos, new Vector3(cellSize, 0, cellSize));
                if (drawLabels)
                {
                    Handles.Label(cells[i, j].WorldPos, cells[i, j].MapPos.ToString());
                }
            }
        }
    }
    
#endif
    
}
