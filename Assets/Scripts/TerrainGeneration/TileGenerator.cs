using System;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
	[SerializeField] private float cutoffCoefficient;
	[SerializeField] private float noiseCoefficient;
	[SerializeField] private TileSO[] availableTiles;

	public Color GetTerrainGizmosColor(Vector2Int mapPos, Vector2Int mapSize)
	{
		/*float noise = Mathf.PerlinNoise(mapPos.x * noiseCoefficient, mapPos.y * noiseCoefficient);
		
		int xOffset = Mathf.Abs(mapPos.x - mapSize.x);
		int yOffset = Mathf.Abs(mapPos.y - mapSize.y);

		float maxXValue = mapSize.x / 2f;
		float maxYValue = mapSize.y / 2f;

		float xDiff = Mathf.Abs(maxXValue - xOffset);
		float yDiff = Mathf.Abs(maxYValue - yOffset);

		float howCloseToEdge = 1 - (xDiff > yDiff ? xDiff / maxXValue : yDiff / maxYValue) * cutoffGradient;

		noise *= howCloseToEdge;

		return new Color(noise, noise, noise);*/
		
		
		TileSO tile = GetTilePrefab(mapPos, mapSize);
		if (tile == null) return Color.black;

		return tile.gizmosColor;
	}
	
	public bool TryGetTerrainPrefab(Vector2Int mapPos, Vector2Int mapSize, out GameObject terrainPrefab)
	{
		TileSO tileSO = GetTilePrefab(mapPos, mapSize);
		terrainPrefab = tileSO == null ? null : tileSO.terrainPrefab;
		
		return terrainPrefab != null;
	}

	private TileSO GetTilePrefab(Vector2Int mapPos, Vector2Int mapSize)
	{
		float noise = Mathf.PerlinNoise(mapPos.x * noiseCoefficient, mapPos.y * noiseCoefficient);
		
		int xOffset = Mathf.Abs(mapPos.x - mapSize.x);
		int yOffset = Mathf.Abs(mapPos.y - mapSize.y);

		float maxXValue = mapSize.x / 2f;
		float maxYValue = mapSize.y / 2f;

		float xDiff = Mathf.Abs(maxXValue - xOffset);
		float yDiff = Mathf.Abs(maxYValue - yOffset);

		float howCloseToEdge = 1 - (xDiff > yDiff ? xDiff / maxXValue : yDiff / maxYValue) * cutoffCoefficient;

		noise *= howCloseToEdge;

		foreach (TileSO tile in availableTiles)
		{
			if (tile.range.x < noise && tile.range.y > noise)
			{
				return tile;
			}
		}
		
		//Debug.LogError("Tile not found");
		return null;
	}
}
