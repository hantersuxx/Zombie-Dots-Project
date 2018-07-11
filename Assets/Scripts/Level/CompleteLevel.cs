using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour
{
    LevelManager LevelManager => LevelManager.Instance;
    LevelVariables LevelVariables => LevelVariables.Instance;

    [SerializeField]
    private Text scores;
    public Text Scores => scores;

    [SerializeField]
    private Text hpLeft;
    public Text HpLeft => hpLeft;

    private void OnEnable()
    {
        //StartCoroutine(AnimateText(Scores, LevelVariables.Score));
        //StartCoroutine(AnimateText(HpLeft, LevelVariables.CurrentHealth));

        Scores.text = LevelVariables.Score.ToString();
        HpLeft.text = LevelVariables.CurrentHealth.ToString();
    }

    public void Continue()
    {
        Extensions.Log(GetType(), "Continue pressed");
        if (LevelsManager.Instance.NextLevelData != null)
        {
            LevelManager.ToggleCompleteLevel();
            LevelManager.SceneData.Storage.SceneFader.FadeTo(LevelsManager.Instance.NextLevelData.AssociatedScene);
        }
        else
        {
            Menu();
        }
    }

    public void Menu()
    {
        Extensions.Log(GetType(), "Menu pressed");
        LevelManager.ToggleCompleteLevel();
        LevelManager.SceneData.Storage.SceneFader.FadeTo(LevelManager.SceneData.SceneNode.Parent.Data);
    }

    private IEnumerator AnimateText(Text field, int maxValue)
    {
        int displayValue = 0;
        field.text = displayValue.ToString();

        yield return new WaitForSeconds(.7f);

        while (displayValue < maxValue)
        {
            displayValue++;
            field.text = displayValue.ToString();

            yield return new WaitForSeconds(.05f);
        }
    }
}
