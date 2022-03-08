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
        float n = -mousePos.y / (mousePos - camera.transform.position).y;

        Vector3 mouseGridPos = mousePos +
                               n * (mousePos - camera.transform.position);
        Vector3 objPosition = gridMap.GetSnappedPosition(mouseGridPos);
        objectToPlace.transform.position = objPosition;
        Debug.DrawLine(mouseGridPos, mouseGridPos + Vector3.up, Color.red);
        Debug.Log(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(objectToPlace, objPosition, Quaternion.identity);
        }
    }
}
