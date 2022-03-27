using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceSO))]
public class ResourceSOInspector : Editor
{
    private SerializedObject so;

    private SerializedProperty propQuantity;
    private SerializedProperty propGeneration;
    private SerializedProperty propPrefabs;
    private SerializedProperty propNoiseCoef;
    private SerializedProperty propNoiseClip;
    private SerializedProperty propProbability;
    private SerializedProperty propProducingMat;
    private SerializedProperty propConsumingMat;
    
    private void OnEnable()
    {
        so = serializedObject;
        
        propQuantity = so.FindProperty("quantity");
        propGeneration = so.FindProperty("generationType");
        propPrefabs = so.FindProperty("prefabs");
        propNoiseCoef = so.FindProperty("noiseCoefficient");
        propNoiseClip = so.FindProperty("noiseClip");
        propProbability = so.FindProperty("probability");
        propProducingMat = so.FindProperty("producingMat");
        propConsumingMat = so.FindProperty("consumingMat");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Universal Properties");
        EditorGUILayout.PropertyField(propQuantity);
        EditorGUILayout.PropertyField(propPrefabs);
        EditorGUILayout.PropertyField(propProducingMat);
        EditorGUILayout.PropertyField(propConsumingMat);
        EditorGUILayout.PropertyField(propGeneration);

        EditorGUILayout.Space();

        switch ((ResourceSO.Generation) propGeneration.enumValueIndex)
        {
            case (ResourceSO.Generation.NOISE):
            {
                EditorGUILayout.PropertyField(propNoiseCoef);
                EditorGUILayout.PropertyField(propNoiseClip);
                
                break;
            }
            case (ResourceSO.Generation.RANDOM):
            {
                EditorGUILayout.PropertyField(propProbability);
                break;
            }
            case (ResourceSO.Generation.NOT_GENERATED):
            {
                break;
            }
        }

        so.ApplyModifiedProperties();
    }
}
