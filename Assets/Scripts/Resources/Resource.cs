using System;
using System.Collections;
using UnityEngine;

public class Resource : MonoBehaviour
{
	public event Action<ResourceSO, int> collectionFinished;
	
	[SerializeField] private GameObject collectionMarker;
	
	private float timeToCollect = 5f;
	private ResourceSO resourceData;
	private int quantity = 10;
	
	public void Init(ResourceSO resourceSO, int quantity, float timeToCollect)
	{
		this.resourceData = resourceSO;
		this.quantity = quantity;
		this.timeToCollect = timeToCollect;
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
}
