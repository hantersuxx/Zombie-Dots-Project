using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSave : MonoBehaviour
{
    public Vault Vault => GameObject.FindGameObjectWithTag(Tags.Vault).GetComponent<Vault>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == Tags.Vault)
        {
            LevelManager.Instance.AddScore(1);
            LevelManager.Instance.AchieveGoal();
            ObjectPooler.Instance.Destroy(Tags.Human, gameObject);
        }
    }
}
