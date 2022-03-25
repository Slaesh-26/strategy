using System;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material markerNegative;
    [SerializeField] private Material markerPositive;

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetDefaultVisuals()
    {
        meshRenderer.enabled = false;
    }

    public void SetParent(Transform t)
    {
        transform.SetParent(t);
    }

    public void SetMarkerPositive()
    {
        ReplaceAllMaterials(markerPositive);
    }

    public void SetMarkerState(bool isPositive)
    {
        if (isPositive)
        {
            ReplaceAllMaterials(markerPositive);
        }
    }

    private void ReplaceAllMaterials(Material mat)
    {
        Material[] materials = new Material[meshRenderer.materials.Length];
        
        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            materials[i] = mat;
        }

        meshRenderer.materials = materials;
    }
}
