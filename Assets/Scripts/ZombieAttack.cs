﻿using System;
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
    public VaultHealth VaultHealth => GameManager.Instance.Vault.GetComponent<VaultHealth>();
    public bool TimerActive { get; private set; } = false;
    public float TimerSeconds { get; private set; } = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == Tags.Vault)
        {
            VaultHealth.TakeDamage(AttackAmount);
            GameManager.Instance.DestroyObject(gameObject);
        }
        else if (obj.tag == Tags.Human)
        {
            if (TimerActive && obj.GetComponent<StateController>().IsActive)
            {
                TimerSeconds += Time.deltaTime;
                if (TimerSeconds >= TurnToZombieTimeout)
                {
                    obj.SetActive(false);
                    Vector3 objPos = obj.transform.position;
                    GameManager.Instance.DestroyObject(obj);
                    GameManager.Instance.CreateObject(GameManager.Instance.ZombiePrefab, objPos);
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

    private void Instantiate(GameObject gameObject, Vector3? position, Quaternion identity)
    {
        var instance = Instantiate<GameObject>(gameObject, position.Value, identity);
        instance.GetComponent<ZombieController>().SetupAI(true);
    }
}
