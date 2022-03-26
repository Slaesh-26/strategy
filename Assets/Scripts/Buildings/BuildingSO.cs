using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingSO : ScriptableObject
{
    /*public List<CraftComponent> recipe => new List<CraftComponent>(recipe);
    public GameObject modelPrefab => modelModelPrefab;
    
    public float resourcesConsumeDelay => resourcesConsumeDelay;
    public float resourcesProduceDelay => resourcesProduceDelay;
    public int resourcesConsumeQuantity => resourcesConsumeQuantity;
    public int resourcesProduceQuantity => resourcesProduceQuantity;

    public ResourceSO[] resourcesToConsume => resourcesToConsume;
    public ResourceSO[] resourcesToProduce => resourcesToProduce;*/
    
    public List<CraftComponent> recipe;
    public GameObject modelPrefab;
    [Space]
    public float resourcesConsumeDelay;
    public float resourcesProduceDelay;
    public int resourcesConsumeQuantity;
    public int resourcesProduceQuantity;
    [Space]
    public ResourceSO[] resourcesToConsume;
    public ResourceSO[] resourcesToProduce;
}

[Serializable]
public struct CraftComponent
{
    public ResourceSO resourceSO;
    
    [Min(1)]
    public int quantity;
}