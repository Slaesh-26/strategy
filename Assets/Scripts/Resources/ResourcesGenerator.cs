using UnityEngine;

public class ResourcesGenerator : MonoBehaviour
{
	[SerializeField] private ResourceSO[] resources;
	[SerializeField] private Resource resourcePrefab;

	public Resource CreateResource(Vector2Int mapPos, Vector3 worldPos, Vector2Int mapSize)
	{
		foreach (ResourceSO resourceData in resources)
		{
			if (resourceData.generationType == ResourceSO.Generation.NOISE)
			{
				float noise = Mathf.PerlinNoise(mapPos.x * resourceData.noiseCoefficient,
				                                mapPos.y * resourceData.noiseCoefficient);
				if (noise > resourceData.noiseClip)
				{
					Resource res = InstantiateResource(resourceData, worldPos);
					return res;
				}
			}
			if (resourceData.generationType == ResourceSO.Generation.RANDOM)
			{
				bool success = RollTheDice(resourceData.probability);

				if (success)
				{
					Resource res = InstantiateResource(resourceData, worldPos);
					return res;
				}
			}
		}

		return null;
	}

	public Color GetGizmosColor(Vector2Int mapPos, Vector3 worldPos, Vector2Int mapSize)
	{
		foreach (ResourceSO resource in resources)
		{
			if (resource.generationType == ResourceSO.Generation.NOISE)
			{
				float noise = Mathf.PerlinNoise(mapPos.x * resource.noiseCoefficient,
				                                mapPos.y * resource.noiseCoefficient);
				if (noise > resource.noiseClip)
				{
					return Color.red;
				}
			}
			if (resource.generationType == ResourceSO.Generation.RANDOM)
			{
				bool success = RollTheDice(resource.probability);
				return success ? Color.red : Color.black;
			}
		}

		return Color.black;
	}

	private Resource InstantiateResource(ResourceSO resourceData, Vector3 worldPos)
	{
		Resource res = Instantiate(resourcePrefab, worldPos, MathUtils.GetRandomFullCircleRotation());
		res.SetParent(transform);
		res.Init(resourceData);
		return res;
	}
	
	private bool RollTheDice(float probability)
	{
		return Random.Range(0f, 1f) < probability;
	}
}
