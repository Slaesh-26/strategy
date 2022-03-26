using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Resource : GridObject
{
	public event Action<ResourceSO, int> collectionFinished;
	
	[SerializeField] private GameObject collectionMarker;

	private ResourceSO resourceData;
	private float timeToCollect = 5f;
	private int quantity = 10;
	
	public void Init(ResourceSO resourceSO)
	{
		this.resourceData = resourceSO;

		GameObject randomPrefab = resourceSO.GetRandomPrefab();
		InitVisuals(randomPrefab);
	}
	
	public void BeginCollect()
	{
		StartCoroutine(CollectCoroutine());
	}

	public void CancelCollection()
	{
		StopAllCoroutines();
	}

	private IEnumerator CollectCoroutine()
	{
		collectionMarker.SetActive(true);
		yield return new WaitForSeconds(timeToCollect);
		collectionFinished?.Invoke(resourceData, quantity);
		collectionMarker.SetActive(false);
	}

	private void OnDrawGizmos()
	{
		Handles.Label(transform.position, resourceData.name + quantity.ToString());
	}
}
