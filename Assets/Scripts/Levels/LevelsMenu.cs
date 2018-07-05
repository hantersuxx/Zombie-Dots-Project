using UnityEngine;

public class LevelsMenu : MonoBehaviour
{
    LevelsManager LevelsManager => LevelsManager.Instance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Extensions.Log(GetType(), "Back pressed");
            LevelsManager.SceneData.Storage.SceneFader.FadeTo(LevelsManager.SceneData.SceneNode.Parent.Data);
        }
    }
}
