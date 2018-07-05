using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsContent : MonoBehaviour
{
    private void Start()
    {
        LevelsManager.Instance.InitializeContentView();
    }
}
