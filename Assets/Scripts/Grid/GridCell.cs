using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public bool IsPlaceable => !IsOccupied && IsGround;
    
    public Vector2Int MapPos { get; }
    public Vector3 WorldPos { get; }
    public bool IsOccupied { get; private set; }
    public bool IsGround { get; private set; }

    private List<GridObject> gridObjects;
    
    public GridCell(Vector2Int mapPos, Vector3 worldPos)
    {
        MapPos = mapPos;
        WorldPos = worldPos;
        IsOccupied = false;
    }

    public bool ContainsGridObject<T>() where T : GridObject
    {
        GridObject obj = gridObjects.Find(o => o is T);
        return obj != null;
    }

    public void AddObject(GridObject gridObject)
    {
        if (gridObjects == null)
        {
            gridObjects = new List<GridObject>();
        }
        
        gridObjects.Add(gridObject);
        IsOccupied = true;
    }
    
    public void RemoveObject(GridObject gridObject)
    {
        gridObjects.Remove(gridObject);
        
        if (gridObjects.Count == 0)
        {
            IsOccupied = false;
        }
    }

    public void SetPlacementActive(bool active)
    {
        IsGround = active;
    }

    public CellData GetCellData()
    {
        return new CellData()
        {
            mapPos = MapPos,
            worldPos = WorldPos,
            isOccupied = IsOccupied,
            isGround = IsGround,
            isPlaceable = IsPlaceable
        };
    }

    public List<GridObject> GetCellGridObjects()
    {
        return new List<GridObject>(gridObjects);
    }
}

public struct CellData
{
    public Vector2Int mapPos;
    public Vector3 worldPos;
    public bool isOccupied;
    public bool isGround;
    public bool isPlaceable;
}