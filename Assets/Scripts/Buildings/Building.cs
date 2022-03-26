using System.Collections;
using UnityEngine;

public class Building : GridObject
{
	[SerializeField] private ParticleSystem particles;

	private ParticleSystemRenderer particlesRenderer;
	private BuildingSO buildingData;
	
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
		while (true)
		{
			yield return new WaitForSeconds(buildingData.resourcesConsumeDelay);
            ConsumeResources();
		}
	}

	private IEnumerator ProduceResourcesCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(buildingData.resourcesProduceDelay);
            ProduceResources();
		}
	}

	private void ProduceResources()
	{
		foreach (ResourceSO resource in buildingData.resourcesToProduce)
		{
			resource.AddQuantity(buildingData.resourcesProduceQuantity);
			print($"Produced {buildingData.resourcesProduceQuantity} of {resource.name}");
			
			//PlayParticleEffect(resource.producingMat);
		}
	}
	
	private void ConsumeResources()
	{
		foreach (ResourceSO resource in buildingData.resourcesToConsume)
		{
			if (resource.RemoveQuantity(buildingData.resourcesConsumeQuantity))
			{
				print($"Consumed {buildingData.resourcesConsumeQuantity} of {resource.name}");
				
				//PlayParticleEffect(resource.consumingMat);
			}
			else
			{
				print($"No enough {resource.name} to continue");
				StopAllCoroutines();
			}
		}
	}

	private void PlayParticleEffect(Material mat)
	{
		if (particles.isPlaying) particles.Stop();
		
		particlesRenderer.material = mat;
		particles.Play();
	}
}
