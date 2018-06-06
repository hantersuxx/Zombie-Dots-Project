using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField]
    private float turnToZombieTimeout = 0.5f;
    [SerializeField]
    private int attackAmount = 1;

    public float TurnToZombieTimeout => turnToZombieTimeout;
    public int AttackAmount => attackAmount;
    public bool TimerActive { get; private set; } = false;
    public float TimerSeconds { get; private set; } = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (LevelManager.Instance.GameIsPaused || LevelManager.Instance.GameIsOver || LevelManager.Instance.LevelFinished)
        {
            return;
        }

        var obj = collision.gameObject;
        if (obj.tag == Tags.Vault)
        {
            OnVaultStay();
        }
        else if (obj.tag == Tags.Human)
        {
            OnHumanStay(obj);
        }
    }

    private void OnVaultStay()
    {
        LevelManager.Instance.TakeDamage(AttackAmount);
        LevelManager.Instance.AddScore(Globals.MissZombieScore);
        ObjectPooler.Instance.Destroy(Tags.Zombie, gameObject);
    }

    private void OnHumanStay(GameObject obj)
    {
        if (TimerActive && obj.GetComponent<StateController>().IsActive)
        {
            TimerSeconds += Time.deltaTime;
            if (TimerSeconds >= TurnToZombieTimeout)
            {
                obj.SetActive(false);
                Vector3 objPos = obj.transform.position;
                LevelManager.Instance.AddScore(Globals.KillHumanScore);
                ObjectPooler.Instance.Destroy(Tags.Human, obj);
                ObjectPooler.Instance.SpawnFromPool(Tags.Zombie, objPos);
                ResetTimer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TimerActive = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        TimerActive = false;
        TimerSeconds = 0;
    }
}
