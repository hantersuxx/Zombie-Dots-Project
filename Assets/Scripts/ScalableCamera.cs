using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableCamera : MonoBehaviour
{
    public float tileSizeInPixels = 64f;

    public Camera Camera => GetComponent<Camera>();

    void Start()
    {
        Camera.orthographicSize = tileSizeInPixels * Screen.height / Screen.width * 0.25f;
    }
}
