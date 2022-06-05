using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class ResourceSO : ScriptableObject
{
    public event Action<int> changed;

    public Material producingMat;
    public Material consumingMat;
    public GameObject[] prefabs;

    public int quantity;
    public Generation generationType;
    public float noiseCoefficient = 0.05f;
    public float noiseClip = 0.1f;
    public float probability = 0.05f;

    public enum Generation
    {
        RANDOM,
        NOISE,
        NOT_GENERATED
    }
    
    public int GetQuantity()
    {
        return quantity;
    }

    public GameObject GetRandomPrefab()
    {
        int index = Random.Range(0, prefabs.Length);
        return prefabs[index];
    }

    public void AddQuantity(int quantityToAdd)
    {
        quantity += quantityToAdd;
        changed?.Invoke(quantity);
    }
    
    public bool RemoveQuantity(int quantityToRemove)
    {
        int newQuantity = quantity - quantityToRemove;
        if (newQuantity < 0)
        {
            Debug.LogWarning("Quantity cannot be negative");
            return false;
        }
        quantity = Mathf.Clamp(newQuantity, 0, Int32.MaxValue);
        changed?.Invoke(quantity);

        return true;
    }
}
