using UnityEngine;

public class ResourcesGenerator : MonoBehaviour
{
	[SerializeField] private ResourceSO[] resources;

	public GameObject GetResourcePrefab(Vector2Int mapPos, Vector2Int mapSize, out Color color)
	{
		foreach (ResourceSO resource in resources)
		{
			if (resource.GenerationType == ResourceSO.Generation.NOISE)
			{
				float noise = Mathf.PerlinNoise(mapPos.x * resource.NoiseCoefficient,
				                                mapPos.y * resource.NoiseCoefficient);
				if (noise > resource.NoiseClip)
				{
					color = Color.red;
					return resource.Prefabs[Random.Range(0, resource.Prefabs.Length)];
				}
			}
			if (resource.GenerationType == ResourceSO.Generation.RANDOM)
			{
				bool success = RollTheDice(resource.Probability);
				color = success ? Color.red : Color.black;
				return success ? resource.Prefabs[0] : null;
			}
		}

		color = Color.black;
		return null;
	}

	private bool RollTheDice(float probability)
	{
		return Random.Range(0f, 1f) < probability;
	}
}
