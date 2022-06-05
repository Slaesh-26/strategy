using System;
using UnityEngine;

[CreateAssetMenu]
public class TileSO : ScriptableObject
{
	public GameObject terrainPrefab;
	public Color gizmosColor;
	public Range range;
	public float howNearToShore;
}

[Serializable]
public struct Range
{
	public float min;
	public float max;

	public Range(float min, float max)
	{
		this.min = min;
		this.max = max;
	}
}
