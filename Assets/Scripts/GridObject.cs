using System;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Material markerNegative;
    [SerializeField] private Material markerPositive;
    [SerializeField] private Material[] defaultMaterials;

    /*private void OnEnable()
    {
        defaultMaterials = renderer.materials;
    }*/

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetDefaultVisuals()
    {
        renderer.materials = defaultMaterials;
    }

    public void SetMarkerPositive()
    {
        ReplaceAllMaterials(markerPositive);
    }

    public void SetMarkerNegative()
    {
        ReplaceAllMaterials(markerNegative);
    }

    private void ReplaceAllMaterials(Material mat)
    {
        Material[] materials = new Material[renderer.materials.Length];
        
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            materials[i] = mat;
        }

        renderer.materials = materials;
    }
}
