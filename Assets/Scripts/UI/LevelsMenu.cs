using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : BackNavigation
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Extensions.Log(GetType(), "Back pressed");
            NavigateBack(LevelsManager.Instance.Storage.SceneFader);
        }
    }
}
