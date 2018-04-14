using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableCamera : MonoBehaviour
{
    public float tileSizeInPixels = 64f;

    void Start()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        GetComponent<Camera>().orthographicSize = tileSizeInPixels * Screen.height / Screen.width * 0.25f;
    }
}
