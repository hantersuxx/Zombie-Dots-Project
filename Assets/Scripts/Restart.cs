using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public Button Button { get; private set; }

    private void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(RestartOnClick);
    }

    private void RestartOnClick()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
