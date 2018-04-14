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
        float tileSizeInPixels = 64f;
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        GetComponent<Camera>().orthographicSize = tileSizeInPixels * Screen.height / Screen.width * 0.25f;
    }
}
