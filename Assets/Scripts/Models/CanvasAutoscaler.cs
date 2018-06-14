using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAutoscaler : MonoBehaviour
{
    private void Start()
    {
        CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
        canvasScaler.referenceResolution = new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
