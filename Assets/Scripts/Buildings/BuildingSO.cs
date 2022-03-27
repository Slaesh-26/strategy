using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingSO : ScriptableObject
{
    public List<CraftComponent> recipe;
    public GameObject modelPrefab;
    public GameObject markerPrefab;
    [Space]
    public float resourcesConsumeDelay;
    public float resourcesProduceDelay;
    public int resourcesConsumeQuantity;
    public int resourcesProduceQuantity;
    [Space]
    public ResourceSO[] resourcesToConsume;
    public ResourceSO[] resourcesToProduce;
    [Space]
    public Sprite uiImage;
}

[Serializable]
public struct CraftComponent
{
    public ResourceSO resourceSO;
    
    [Min(1)]
    public int quantity;
}