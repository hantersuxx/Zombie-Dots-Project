using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : ZombieController
{
    [SerializeField]
    private List<Sprite> states;
    public List<Sprite> States { get => states; private set => states = value; }

    int currentState { get; set; } = 0;

    protected override void OnTakingDamage()
    {
        float statesCount = States.Count + 1;
        float portion = BaseHealth / statesCount;
        if (CurrentHealth <= portion * (States.Count - currentState))
        {
            GetComponent<SpriteRenderer>().sprite = States[currentState];
            SpawnParticles();
            currentState++;
        }
    }
}
