using System;
using System.Collections;
using UnityEngine;

public class Building : GridObject
{
	[SerializeField] private ParticleSystem particles;

	private ParticleSystemRenderer particlesRenderer;
	private BuildingSO buildingData;
	private bool enoughResources = true;
	
	public void Init(BuildingSO buildingSO)
	{
		buildingData = buildingSO;
		InitVisuals(buildingSO.modelPrefab);

		particlesRenderer = particles.GetComponent<ParticleSystemRenderer>();
		
		StartCoroutine(ConsumeResourcesCoroutine());
		StartCoroutine(ProduceResourcesCoroutine());
	}

	private IEnumerator ConsumeResourcesCoroutine()
	{
		if (buildingData.resourcesToConsume == null || buildingData.resourcesToConsume.Length == 0)
		{
			yield break;
		}
		
		yield return new WaitForSeconds(buildingData.resourcesConsumeDelay);

		while (enoughResources)
		{
			foreach (ResourceSO resource in buildingData.resourcesToConsume)
			{
				if (resource.RemoveQuantity(buildingData.resourcesConsumeQuantity))
				{
					print($"Consumed {buildingData.resourcesConsumeQuantity} of {resource.name}");
				
					PlayParticleEffect(resource.consumingMat);
					yield return new WaitForSeconds(buildingData.resourcesConsumeDelay);
				}
				else
				{
					print($"No enough {resource.name} to continue");
					enoughResources = false;
				}
			}
		}
	}

	private IEnumerator ProduceResourcesCoroutine()
	{
		if (buildingData.resourcesToProduce == null || buildingData.resourcesToProduce.Length == 0)
		{
			yield break;
		}
		
		yield return new WaitForSeconds(buildingData.resourcesProduceDelay);
		
		while (enoughResources)
		{
			foreach (ResourceSO resource in buildingData.resourcesToProduce)
			{
				resource.AddQuantity(buildingData.resourcesProduceQuantity);
				print($"Produced {buildingData.resourcesProduceQuantity} of {resource.name}");
			
				PlayParticleEffect(resource.producingMat);
				yield return new WaitForSeconds(buildingData.resourcesProduceDelay);
			}
		}
	}

	private void PlayParticleEffect(Material mat)
	{
		if (particles.particleCount > 0) particles.Clear();
		
		particlesRenderer.material = mat;
		particles.Emit(1);
	}
}
