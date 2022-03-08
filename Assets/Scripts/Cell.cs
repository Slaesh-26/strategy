using UnityEngine;

public class Cell
{
    public Vector3 WorldPos => worldPos;
    public Vector2Int MapPos => mapPos;
    public bool IsOccupied => isOccupied;
    
    private Vector2Int mapPos;
    private Vector3 worldPos;
    private bool isOccupied;
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
}