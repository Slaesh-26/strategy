using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class ResourceSO : ScriptableObject
{
    // todo: cleanup properties
    public event Action<int> changed;
    public Generation GenerationType => generation;
    public float Probability => probability;
    public float NoiseCoefficient => noiseCoefficient;
    public float NoiseClip => noiseClip;
    public GameObject[] Prefabs => prefabs;

    public Material producingMat;
    public Material consumingMat;

    public enum Generation
    {
        RANDOM,
        NOISE,
        NOT_GENERATED
    }
    
    [SerializeField] private int quantity;
    [SerializeField] private Generation generation;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private float noiseCoefficient = 0.05f;
    [SerializeField] private float noiseClip = 0.1f;
    [SerializeField] private float probability = 0.05f;

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
