using UnityEngine;
using UnityEngine.SceneManagement;

public class BackNavigation : MonoBehaviour, IBackNavigable
{
    public void NavigateBack(SceneFader sceneFader)
    {
        string prevScene = GameManager.Instance.GetPreviousScene();
        if (!string.IsNullOrEmpty(prevScene))
        {
            sceneFader.FadeTo(prevScene);
        }
    }
}
