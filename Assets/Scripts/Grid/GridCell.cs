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

    public bool ContainsGridObject<T>(out T gridObject) where T : GridObject
    {
        if (gridObjects == null)
        {
            gridObject = null;
            return false;
        }
        
        GridObject obj = gridObjects.Find(o => o is T);
        bool objFound = obj != null;

        if (objFound) gridObject = (T) obj;
        else gridObject = null;
        
        return objFound;
    }

    public void AddObject(GridObject gridObject)
    {
        gridObjects ??= new List<GridObject>();
        
        gridObjects.Add(gridObject);
        gridObject.destroyed += OnGridObjectDestroyed;
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

    public void SetGround(bool isGround)
    {
        IsGround = isGround;
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

    private void OnGridObjectDestroyed(GridObject gridObject)
    {
        gridObject.destroyed -= OnGridObjectDestroyed;
        RemoveObject(gridObject);
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