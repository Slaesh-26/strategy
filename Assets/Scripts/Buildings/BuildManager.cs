using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
	[SerializeField] private ResourceSO[] resources;
	[SerializeField] private BuildingSO[] buildings;
	[SerializeField] private Building buildingPrefab;
	[SerializeField] private Marker cellStatusMarker;
	
	private int buildingIndex = 0;
	private GameObject buildingMarker;
	private IGridProvider gridProvider;

	public void Init(IGridProvider gridProvider)
	{
		this.gridProvider = gridProvider;
		buildingMarker = Instantiate(buildings[buildingIndex].modelPrefab);
	}

	public void OnGridCellClicked(CellData clickedCell)
	{
		if (!clickedCell.isPlaceable) return;
		
		Build(buildings[buildingIndex], clickedCell.mapPos, buildingMarker.transform.rotation);
	}

	public void OnSelectedCellChanged(CellData clickedCell)
	{
		cellStatusMarker.SetState(clickedCell.isPlaceable);
		cellStatusMarker.SetPosition(clickedCell.worldPos);
		
		buildingMarker.transform.position = clickedCell.worldPos;
	}

	private void Build(BuildingSO buildingSO, Vector2Int mapPos, Quaternion rotation)
	{
		if (IsEnoughResources(buildingSO))
		{
			SpendResources(buildingSO);
			Building building = InstantiateBuilding(buildingSO, rotation);
			gridProvider.TryPlaceGridObject(mapPos, building);
			print("Build");
		}
		else
		{
			print("Can't build, no enough resources");
		}
	}

	private Building InstantiateBuilding(BuildingSO buildingSO, Quaternion rotation)
	{
		Building building = Instantiate(buildingPrefab, Vector3.zero, rotation);
		building.Init(buildingSO);

		return building;
	}

	private void UpdateBuildingMarker()
	{
		Vector3 position = buildingMarker.transform.position;
		Destroy(buildingMarker);
		buildingMarker = Instantiate(buildings[buildingIndex].modelPrefab, position, Quaternion.identity);
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
				buildingMarker = Instantiate(buildings[buildingIndex].modelPrefab, position, Quaternion.identity);
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

	private bool IsEnoughResources(BuildingSO buildingSO)
	{
		List<CraftComponent> craftComponents = buildingSO.recipe;

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
		List<CraftComponent> craftComponents = buildingSO.recipe;

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
