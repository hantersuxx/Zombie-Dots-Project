using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public VaultHealth VaultHealth => GameManager.Instance.Vault.GetComponent<VaultHealth>();
    bool IsTimerActive { get; set; } = false;
    float TimerSeconds { get; set; } = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsTimerActive = true;
        TimerSeconds += Time.deltaTime;
        if (collision.gameObject.tag == Tags.Vault)
        {
            VaultHealth.TakeDamage(1);
            StateController.DestroyInstance(gameObject);
        }
        else if (TimerSeconds >= 1.5f)
        {
            if (collision.gameObject.tag == Tags.Human)
            {
                Instantiate(gameObject, collision?.transform?.position, Quaternion.identity);
                StateController.DestroyInstance(collision.gameObject);
                TimerSeconds = 0;
            }
        }


        IsTimerActive = false;
    }

    private void Instantiate(GameObject gameObject, Vector3? position, Quaternion identity)
    {
        var instance = Instantiate<GameObject>(gameObject, position.Value, identity);
        instance.GetComponent<ZombieController>().SetupAI(true);
    }
}
