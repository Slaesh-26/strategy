using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildingButton : MonoBehaviour
{
	public event Action<BuildingSO> click;
	
	private Button button;
	private BuildingSO building;

	public void Init(BuildingSO buildingSO)
	{
		this.building = buildingSO;
		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		click?.Invoke(building);
	}
}
