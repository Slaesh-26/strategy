using UnityEngine;

public class ResourcesGenerator : MonoBehaviour
{
	[SerializeField] private ResourceSO[] resources;
	[SerializeField] private Resource resourcePrefab;

	public Resource CreateResource(Vector2Int mapPos, Vector3 worldPos, Vector2Int mapSize)
	{
		foreach (ResourceSO resourceData in resources)
		{
			if (resourceData.GenerationType == ResourceSO.Generation.NOISE)
			{
				float noise = Mathf.PerlinNoise(mapPos.x * resourceData.NoiseCoefficient,
				                                mapPos.y * resourceData.NoiseCoefficient);
				if (noise > resourceData.NoiseClip)
				{
					Resource res = InstantiateResource(resourceData, worldPos);
					return res;
				}
			}
			if (resourceData.GenerationType == ResourceSO.Generation.RANDOM)
			{
				bool success = RollTheDice(resourceData.Probability);

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
			if (resource.GenerationType == ResourceSO.Generation.NOISE)
			{
				float noise = Mathf.PerlinNoise(mapPos.x * resource.NoiseCoefficient,
				                                mapPos.y * resource.NoiseCoefficient);
				if (noise > resource.NoiseClip)
				{
					return Color.red;
				}
			}
			if (resource.GenerationType == ResourceSO.Generation.RANDOM)
			{
				bool success = RollTheDice(resource.Probability);
				return success ? Color.red : Color.black;
			}
		}

		return Color.black;
	}

	private Resource InstantiateResource(ResourceSO resourceData, Vector3 worldPos)
	{
		Resource res = Instantiate(resourcePrefab, worldPos, MathUtils.GetRandomFullCircleRotation());
		res.Init(resourceData);
		return res;
	}
	
	private bool RollTheDice(float probability)
	{
		return Random.Range(0f, 1f) < probability;
	}
}
