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
    public Vault Vault => GameObject.FindGameObjectWithTag(Tags.Vault).GetComponent<Vault>();
    public bool TimerActive { get; private set; } = false;
    public float TimerSeconds { get; private set; } = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == Tags.Vault)
        {
            Vault.TakeDamage(AttackAmount);
            LevelManager.Instance.AddScore(-1);
            ObjectPooler.Instance.Destroy(Tags.Zombie, gameObject);
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
                    ObjectPooler.Instance.Destroy(Tags.Human, obj);
                    ObjectPooler.Instance.SpawnFromPool(Tags.Zombie, objPos);
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
