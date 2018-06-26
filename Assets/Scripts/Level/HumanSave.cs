using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSave : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (LevelVariables.Instance.GameIsPaused || LevelVariables.Instance.GameIsOver)
        {
            return;
        }

        var obj = collision.gameObject;
        if (obj.tag == Tags.Vault)
        {
            var controller = gameObject.GetComponent<HumanController>();
            controller.TakeDamage(controller.CurrentHealth);
            LevelManager.Instance.AddScore(Globals.SaveHumanScore);
            LevelManager.Instance.SaveHuman();
            LevelManager.Instance.AchieveGoal();
        }
    }
}
