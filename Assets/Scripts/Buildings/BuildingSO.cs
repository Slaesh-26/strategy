using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingSO : ScriptableObject
{
    public List<CraftComponent> Recipe => new List<CraftComponent>(recipe);
    public GridObject Prefab => buildingPrefab;
    
    [SerializeField] private List<CraftComponent> recipe;
    [SerializeField] private GridObject buildingPrefab;
}

[Serializable]
public struct CraftComponent
{
    public ResourceSO resourceSO;
    
    [Min(1)]
    public int quantity;
}