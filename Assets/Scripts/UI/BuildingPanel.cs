using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanel : MonoBehaviour
{
    public event Action<BuildingSO> selectedBuildingChanged;
    
    [SerializeField] private BuildingSO[] availableBuildings;
    [SerializeField] private RectTransform spawnOrigin;
    [SerializeField] private BuildingButton prefab;

    private List<BuildingButton> buttons;

    public void Init()
    {
        buttons = new List<BuildingButton>();
        
        foreach (BuildingSO building in availableBuildings)
        {
            BuildingButton button = Instantiate(prefab, spawnOrigin);
            button.Init(building);
            buttons.Add(button);

            button.click += OnButtonClick;
        }
    }

    private void OnDisable()
    {
        foreach (BuildingButton button in buttons)
        {
            button.click -= OnButtonClick;
        }
    }

    private void OnButtonClick(BuildingSO building)
    {
        selectedBuildingChanged?.Invoke(building);
    }
}
