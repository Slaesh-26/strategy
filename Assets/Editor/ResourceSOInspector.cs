using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ResourceSO))]
public class ResourceSOInspector : Editor
{
    private SerializedObject so;
    
    private void OnEnable()
    {
        so = serializedObject;
    }

    public override void OnInspectorGUI()
    {
        SerializedProperty propQuantity = so.FindProperty("quantity");
        SerializedProperty propGeneration = so.FindProperty("generation");
        SerializedProperty propPrefabs = so.FindProperty("prefabs");
        SerializedProperty propNoiseCoef = so.FindProperty("noiseCoefficient");
        SerializedProperty propNoiseClip = so.FindProperty("noiseClip");
        SerializedProperty propProbability = so.FindProperty("probability");
        SerializedProperty propProducingMat = so.FindProperty("producingMat");
        SerializedProperty propConsumingMat = so.FindProperty("consumingMat");
        
        EditorGUILayout.LabelField("Universal Properties");
        EditorGUILayout.PropertyField(propQuantity);
        EditorGUILayout.PropertyField(propPrefabs);
        EditorGUILayout.PropertyField(propGeneration);
        EditorGUILayout.PropertyField(propProducingMat);
        EditorGUILayout.PropertyField(propConsumingMat);
        
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
