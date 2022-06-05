using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Resource : GridObject
{
	public bool IsBeingCollected { get; private set; }

	[SerializeField] private ParticleSystem collectionMarker;

	private ResourceSO resourceData;
	private float timeToCollect = 5f;
	private int quantity = 10;
	
	public void Init(ResourceSO resourceSO)
	{
		this.resourceData = resourceSO;

		GameObject randomPrefab = resourceSO.GetRandomPrefab();
		InitVisuals(randomPrefab);
		
		collectionMarker.Stop();
		collectionMarker.Clear();
	}
	
	public void BeginCollect()
	{
		StartCoroutine(CollectCoroutine());
	}

	public void CancelCollection()
	{
		collectionMarker.Stop();
		collectionMarker.Clear();
		IsBeingCollected = false;
		StopAllCoroutines();
	}

	private IEnumerator CollectCoroutine()
	{
		IsBeingCollected = true;
		
		collectionMarker.Play();
		yield return new WaitForSeconds(timeToCollect);
		collectionMarker.Stop();
		
		resourceData.AddQuantity(quantity);
		IsBeingCollected = false;
		
		DestroyGridObject();
	}
}
