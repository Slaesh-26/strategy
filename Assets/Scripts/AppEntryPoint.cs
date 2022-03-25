using System;
using UnityEngine;

public class AppEntryPoint : MonoBehaviour
{
	[SerializeField] private GridMap gridMap;
	[SerializeField] private GridInteractor interactor;
	[SerializeField] private BuildManager buildManager;
	
	private void Start()
	{
		gridMap.Init();
		buildManager.Init(gridMap);
		
		interactor.cellClicked += buildManager.OnGridCellClicked;
		interactor.selectedCellChanged += buildManager.OnSelectedCellChanged;
	}
}
