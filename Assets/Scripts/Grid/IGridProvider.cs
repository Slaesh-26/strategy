using UnityEngine;

public interface IGridProvider
{
	public bool TryPlaceGridObject(Vector2Int mapPos, GridObject objectToPlace, Quaternion rotation);
}
