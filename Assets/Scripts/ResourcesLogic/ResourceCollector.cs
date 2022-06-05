using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
	private IGridProvider gridProvider;
	
	public void Init(IGridProvider gridProvider)
	{
		this.gridProvider = gridProvider;
	}

	public void OnGridCellClick(CellData clickedCell)
	{
		if (clickedCell.isPlaceable) return;
		
		if (gridProvider.CellContainsGridObject<Resource>(clickedCell.mapPos, out Resource res))
		{
			//if (res == null) return;
			
			if (res.IsBeingCollected)
			{
				res.CancelCollection();
			}
			else
			{
				res.BeginCollect();
			}
		}
	}
}
