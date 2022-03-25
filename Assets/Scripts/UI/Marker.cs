using UnityEngine;

public class Marker : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private Material positive;
	[SerializeField] private Material negative;
	[SerializeField] private float verticalOffset = 0.01f;

	public void SetState(bool isPositive)
	{
		meshRenderer.material = isPositive ? positive : negative;
	}

	public void SetPosition(Vector3 position)
	{
		transform.position = position + Vector3.up * verticalOffset;
	}
}
