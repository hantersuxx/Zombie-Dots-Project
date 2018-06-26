using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField]
    private float turnToZombieTimeout = 0.5f;

    float TurnToZombieTimeout => turnToZombieTimeout;
    bool TimerActive { get; set; } = false;
    float TimerSeconds { get; set; } = 0;
    ZombieController ZombieController => GetComponent<ZombieController>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (LevelVariables.Instance.GameIsPaused || LevelVariables.Instance.GameIsOver)
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
        LevelManager.Instance.TakeDamage(ZombieController.Stats.Attack);
        ZombieController.TakeDamage(ZombieController.CurrentHealth);
        CameraManager.Instance.ShakeCamera();
        Vibrator.Vibrate(Globals.VaultAttackDuration);
        LevelManager.Instance.AddScore(Globals.MissZombieScore);
    }

    private void OnHumanStay(GameObject obj)
    {
        var controller = obj.GetComponent<HumanController>();
        if (TimerActive && controller.IsActive)
        {
            TimerSeconds += Time.deltaTime;
            if (TimerSeconds >= TurnToZombieTimeout)
            {
                controller.TakeDamage(ZombieController.Stats.Attack);
                if (controller.IsDead)
                {
                    ObjectPooler.Instance.SpawnFromPool(tag, controller.transform.position);
                    LevelManager.Instance.AddScore(Globals.KillHumanScore);
                    LevelManager.Instance.KillHuman();
                    ResetTimer();
                }
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
