using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class AppEntryPoint : MonoBehaviour
{
	[Header("Managers")]
	[SerializeField] private GridMap gridMap;
	[SerializeField] private GridInteractor interactor;
	[SerializeField] private BuildManager buildManager;
	[SerializeField] private ResourceCollector resourceCollector;

	[Header("Effects")]
	[SerializeField] private CameraShake cameraShake;
	
	private void Start()
	{
		gridMap.Init();
		buildManager.Init(gridMap);
		resourceCollector.Init(gridMap);
		interactor.Init(gridMap.Height, gridMap);
		
		interactor.cellClicked += buildManager.OnGridCellClicked;
		interactor.cellClicked += resourceCollector.OnGridCellClick;
		interactor.selectedCellChanged += buildManager.OnSelectedCellChanged;
		buildManager.build += cameraShake.Shake;
	}
}
