using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField]
    private float turnToZombieTimeout = 1.5f;
    [SerializeField]
    private int attackAmount = 1;

    public float TurnToZombieTimeout => turnToZombieTimeout;
    public int AttackAmount => attackAmount;
    public VaultHealth VaultHealth => GameManager.Instance.Vault.GetComponent<VaultHealth>();
    public bool IsTimerActive { get; private set; } = false;
    public float TimerSeconds { get; private set; } = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsTimerActive = true;
        TimerSeconds += Time.deltaTime;

        if (collision.gameObject.tag == Tags.Vault)
        {
            VaultHealth.TakeDamage(AttackAmount);
            StateController.DestroyInstance(gameObject);
        }
        else if (TimerSeconds >= TurnToZombieTimeout)
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
