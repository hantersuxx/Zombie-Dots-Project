using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasValueUpdater : MonoBehaviour
{
    [SerializeField]
    private Text score;

    public Text Score => score;

    private void Update()
    {
        Score.text = $"Score: {GameManager.Instance.Score}";
    }
}
