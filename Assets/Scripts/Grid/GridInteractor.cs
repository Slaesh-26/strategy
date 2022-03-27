using System;
using System.Collections.Generic;
using UnityEngine;

public class GridInteractor : MonoBehaviour
{
    public event Action<CellData> cellClicked;
    public event Action<CellData> selectedCellChanged;
    
    [SerializeField] private bool isOrthographic;
    [SerializeField] private GridMap gridMap;
    
    private new Camera camera;
    private CellData lastSelectedCell;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        Vector2 screenPos = Input.mousePosition;
        Vector3 mousePos = camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 1f));
        Vector3 mouseRay;
        
        if (isOrthographic)
        {
            mouseRay = camera.transform.forward;
        }
        else
        {
            mouseRay = mousePos - camera.transform.position;
        }
        
        float n = (-mousePos.y + gridMap.Height) / mouseRay.y;

        Vector3 mouseGridWorldPos = mousePos + n * mouseRay;
        CellData currentSelectedCell = gridMap.GetCellData(mouseGridWorldPos);

        if (Input.GetMouseButtonDown(0))
        {
            cellClicked?.Invoke(currentSelectedCell);
        }

        if (!lastSelectedCell.Equals(currentSelectedCell))
        {
            lastSelectedCell = currentSelectedCell;
            selectedCellChanged?.Invoke(currentSelectedCell);
            
            print($"is ground {currentSelectedCell.isGround} is occupied {currentSelectedCell.isOccupied}");
        }
    }
}
