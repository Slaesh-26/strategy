using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MinimapCamera : MonoBehaviour
{
    private void Start()
    {
        //render one frame and disable
        StartCoroutine(WaitToEndOfFrame());
    }

    private IEnumerator WaitToEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        GetComponent<Camera>().enabled = false;
    }
}
