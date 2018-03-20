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
            if (TimerSeconds >= 1)
            {
                Instantiate(gameObject, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                TimerSeconds = 0;
            }
        }
        IsTimerActive = false;
    }
}
