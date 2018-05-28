using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    [SerializeField]
    private GameObject cameraManager;
    [SerializeField]
    private GameObject objectPooler;
    [SerializeField]
    private GameObject boardManager;

    private void Awake()
    {
        InstantiateManager(gameManager, GameManager.Instance);
        InstantiateManager(cameraManager, CameraManager.Instance);
        InstantiateManager(objectPooler, ObjectPooler.Instance);
        InstantiateManager(boardManager, BoardManager.Instance);
    }

    private void InstantiateManager<T>(GameObject gameObject, T staticInstance) where T : class
    {
        if (gameObject != null)
        {
            Instantiate(gameObject);
        }
    }


}
