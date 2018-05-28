using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        var camera = Camera.allCameras[0];
        float tileSizeInPixels = 32f;
        //Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        camera.orthographicSize = tileSizeInPixels * Screen.height / Screen.width * 0.25f;
        float aspectRatio = camera.aspect; //(width divided by height)
        float camSize = camera.orthographicSize; //The size value mentioned earlier
        float correctPositionX = aspectRatio * camSize;
        camera.transform.position = new Vector3(correctPositionX - 0.5f, camSize - 0.5f, -1);
    }
}
