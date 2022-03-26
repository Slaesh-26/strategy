using System;
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    protected GameObject model;

    protected void InitVisuals(GameObject modelPrefab)
    {
        model = Instantiate(modelPrefab, transform);
    }
    
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetParent(Transform t)
    {
        transform.SetParent(t);
    }

    /*public void SetDefaultVisuals()
    {
        meshRenderer.enabled = false;
    }*/


    /*public void SetMarkerPositive()
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

    protected void ReplaceAllMaterials(Material mat)
    {
        Material[] materials = new Material[meshRenderer.materials.Length];
        
        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            materials[i] = mat;
        }

        meshRenderer.materials = materials;
    }*/
}
