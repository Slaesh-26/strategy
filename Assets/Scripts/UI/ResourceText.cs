using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceText : MonoBehaviour
{
    [SerializeField] private ResourceSO resourceSO;
    [SerializeField] private TextMeshProUGUI tmPro;
    [SerializeField] private string prefix;

    private void OnEnable()
    {
        resourceSO.changed += OnValueChanged;
        OnValueChanged(resourceSO.GetQuantity());
    }

    private void OnDisable()
    {
        resourceSO.changed -= OnValueChanged;
    }

    private void OnValueChanged(int value)
    {
        tmPro.text = prefix + value.ToString();
    }
}
