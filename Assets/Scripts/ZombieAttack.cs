using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    bool IsTimerActive { get; set; } = false;
    float TimerSeconds { get; set; } = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsTimerActive = true;
        TimerSeconds += Time.deltaTime;
        if (collision.gameObject.tag == Tags.Human)
        {
            if (TimerSeconds >= 0.75f)
            {
                Instantiate(gameObject, collision?.transform?.position, Quaternion.identity);
                Destroy(collision.gameObject);
                TimerSeconds = 0;
            }
        }
        IsTimerActive = false;
    }

    private void Instantiate(GameObject gameObject, Vector3? position, Quaternion identity)
    {
        var instance = Instantiate<GameObject>(gameObject, position.Value, identity);
        instance.GetComponent<StateController>().SetupAI(true);
    }
}
