using System;
using System.Collections.Generic;
using UnityEngine;

public class GridInteractor : MonoBehaviour
{
    public event Action<CellData> cellClicked;
    public event Action<CellData> selectedCellChanged;
    
    private bool _isOrthographic;
    private IGridProvider _gridProvider;
    private Camera _camera;
    private CellData _lastSelectedCell;
    private float _gridMapWorldHeight;
    
    public void Init(float gridMapWorldHeight, IGridProvider gridProvider)
    {
        _camera = Camera.main;
        _isOrthographic = _camera.orthographic;
        _gridProvider = gridProvider;
        _gridMapWorldHeight = gridMapWorldHeight;
    }

    private void Update()
    {
        Vector2 screenPos = Input.mousePosition;
        Vector3 mousePos = _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 1f));
        Vector3 mouseRay;
        
        if (_isOrthographic)
        {
            mouseRay = _camera.transform.forward;
        }
        else
        {
            mouseRay = mousePos - _camera.transform.position;
        }
        
        float n = (-mousePos.y + _gridMapWorldHeight) / mouseRay.y;

        Vector3 mouseGridWorldPos = mousePos + n * mouseRay;
        CellData currentSelectedCell = _gridProvider.GetCellData(mouseGridWorldPos);

        if (Input.GetMouseButtonDown(0))
        {
            cellClicked?.Invoke(currentSelectedCell);
        }

        if (!_lastSelectedCell.Equals(currentSelectedCell))
        {
            _lastSelectedCell = currentSelectedCell;
            selectedCellChanged?.Invoke(currentSelectedCell);
        }
    }
}
