using UnityEngine;

public interface IGridProvider
{
	public bool TryPlaceGridObject(Vector2Int mapPos, GridObject objectToPlace);
	public bool CellContainsGridObject<T>(Vector2Int mapPos) where T : GridObject;
}
