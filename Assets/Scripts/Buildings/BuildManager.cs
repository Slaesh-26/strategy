using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
	[SerializeField] private ResourceSO[] resources;
	[SerializeField] private BuildingSO[] buildings;
	[SerializeField] private Marker cellStatusMarker;
	
	private int buildingIndex = 0;
	private GridObject buildingMarker;
	private IGridProvider gridProvider;

	public void Init(IGridProvider gridProvider)
	{
		this.gridProvider = gridProvider;
		buildingMarker = Instantiate(buildings[buildingIndex].Prefab);
	}

	public void OnGridCellClicked(CellData clickedCell)
	{
		Build(buildings[buildingIndex], clickedCell.mapPos, buildingMarker.transform.rotation);
	}

	public void OnSelectedCellChanged(CellData clickedCell)
	{
		cellStatusMarker.SetState(clickedCell.isPlaceable);
		cellStatusMarker.SetPosition(clickedCell.worldPos);
		buildingMarker.SetPosition(clickedCell.worldPos);
	}

	private void Build(BuildingSO buildingSO, Vector2Int mapPos, Quaternion rotation)
	{
		if (CanBuild(buildingSO))
		{
			SpendResources(buildingSO);
			//build?.Invoke(buildingSO, worldPos, rotation);
			gridProvider.TryPlaceGridObject(mapPos, buildingSO.Prefab, rotation);
			print("Build");
		}
		else
		{
			print("Can't build, no enough resources");
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			RotateMarker();
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			int newIndex = buildingIndex + 1;
			newIndex = ClampInversed(newIndex, 0, buildings.Length - 1);
			if (newIndex != buildingIndex)
			{
				Vector3 position = buildingMarker.transform.position;
				Destroy(buildingMarker.gameObject);
				buildingIndex = newIndex;
				buildingMarker = Instantiate(buildings[buildingIndex].Prefab, position, Quaternion.identity);
			}
		}
	}
	
	private void RotateMarker()
	{
		buildingMarker.transform.Rotate(Vector3.up, 90f, Space.Self);
	}
	
	private int ClampInversed(int value, int min, int max)
	{
		if (value > max) return min;
		if (value < min) return max;
        
		return value;
	}

	private bool CanBuild(BuildingSO buildingSO)
	{
		List<CraftComponent> craftComponents = buildingSO.Recipe;

		foreach (CraftComponent component in craftComponents)
		{
			if (GetResourceQuantity(component.resourceSO) < component.quantity)
			{
				return false;
			}
		}

		return true;
	}

	private void SpendResources(BuildingSO buildingSO)
	{
		List<CraftComponent> craftComponents = buildingSO.Recipe;

		foreach (CraftComponent component in craftComponents)
		{
			component.resourceSO.RemoveQuantity(component.quantity);	
		}
	}

	private int GetResourceQuantity(ResourceSO resourceSO)
	{
		ResourceSO resource = Array.Find(resources, r => r.Equals(resourceSO));

		if (resource == null)
		{
			Debug.LogWarning("Resource not found");
			return 0;
		}

		return resource.GetQuantity();
	}
}
