using UnityEngine;

[CreateAssetMenu]
public class TileSO : ScriptableObject
{
	public GameObject terrainPrefab;
	public Color gizmosColor;
	public Vector2 range; // x - min, y - max
	public float howNearToShore;
}
