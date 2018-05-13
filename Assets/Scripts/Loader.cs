using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private GameObject boardManager;
    [SerializeField]
    private GameObject gameManager;

    private void Awake()
    {
        SetupCamera();
        if (BoardManager.Instance == null)
        {
            Instantiate(boardManager);
        }
        if (GameManager.Instance == null)
        {
            Instantiate(gameManager);
        }
    }

    private void SetupCamera()
    {
        var camera = GetComponent<Camera>();
        float tileSizeInPixels = 64f;
        //Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        camera.orthographicSize = tileSizeInPixels * Screen.height / Screen.width * 0.25f;
        float aspectRatio = camera.aspect; //(width divided by height)
        float camSize = camera.orthographicSize; //The size value mentioned earlier
        float correctPositionX = aspectRatio * camSize;
        camera.transform.position = new Vector3(correctPositionX - 0.5f, camSize - 0.5f, -1);
    }
}
