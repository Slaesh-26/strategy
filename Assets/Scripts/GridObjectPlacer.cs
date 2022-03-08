using System;
using UnityEngine;

public class GridObjectPlacer : MonoBehaviour
{
    [SerializeField] private GridObject objectToPlace;
    [SerializeField] private GridMap gridMap;
    private new Camera camera;
    
    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        Vector2 screenPos = Input.mousePosition;
        Vector3 mousePos = camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 1f));
        Vector3 mouseRay = mousePos - camera.transform.position;
        float n = (-mousePos.y + gridMap.Height) / mouseRay.y;

        Vector3 mouseGridPos = mousePos + n * mouseRay;
        Vector3 objSnappedPosition = gridMap.GetSnappedPosition(mouseGridPos);
        objectToPlace.transform.position = objSnappedPosition;

        if (Input.GetMouseButtonDown(0))
        {
            gridMap.PlaceGridObject(objSnappedPosition, objectToPlace);
        }

        if (gridMap.IsCellOccupied(objSnappedPosition))
        {
            //Debug.DrawLine(objSnappedPosition, objSnappedPosition + Vector3.up * 6f, Color.red);
            objectToPlace.SetMarkerNegative();
        }
        else
        {
            //Debug.DrawLine(objSnappedPosition, objSnappedPosition + Vector3.up * 6f, Color.green);
            objectToPlace.SetMarkerPositive();
        }
    }
}
