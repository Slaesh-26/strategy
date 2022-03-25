using UnityEngine;

public class Cell
{
    public Vector3 WorldPos => worldPos;
    public Vector2Int MapPos => mapPos;
    public bool IsOccupied => isOccupied;
    public bool IsGround => isGround;
    public bool IsCellPlaceable => !isOccupied && isGround;
    
    private Vector2Int mapPos;
    private Vector3 worldPos;
    private bool isOccupied;
    private bool isGround;
    private GridObject gridObject;
    
    public Cell(Vector2Int mapPos, Vector3 worldPos)
    {
        this.mapPos = mapPos;
        this.worldPos = worldPos;
        this.isOccupied = false;
    }

    public void SetObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
        isOccupied = true;
    }
    
    public void RemoveObject()
    {
        this.gridObject = null;
        isOccupied = false;
    }

    public void SetPlacementActive(bool active)
    {
        isGround = active;
    }

    public CellData GetCellData()
    {
        return new CellData()
        {
            mapPos = this.mapPos,
            worldPos = this.worldPos,
            isOccupied = this.isOccupied,
            isGround = this.isGround,
            gridObject = this.gridObject,
            isPlaceable = !this.isOccupied && this.isGround
        };
    }
}

public struct CellData
{
    public Vector2Int mapPos;
    public Vector3 worldPos;
    public bool isOccupied;
    public bool isGround;
    public bool isPlaceable;
    public GridObject gridObject;
}