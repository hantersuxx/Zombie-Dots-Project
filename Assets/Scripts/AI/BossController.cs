using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : ZombieController
{
    [SerializeField]
    private List<Sprite> states;
    public List<Sprite> States { get => states; private set => states = value; }

    int currentState { get; set; } = 0;

    protected override void HandleTakingDamage(object sender, EventArgs e)
    {
        base.HandleTakingDamage(sender, e);

        float statesCount = States.Count;
        float portion = Stats.BaseHealth / statesCount;
        if (CurrentHealth <= portion * (States.Count - currentState) && currentState < States.Count)
        {
            GetComponent<SpriteRenderer>().sprite = States[currentState];
            SpawnParticles(Globals.ZombieParticleHexColor);
            currentState++;
        }
    }

    public override void HandleObjectSpawn(object value)
    {
        base.HandleObjectSpawn(value);
    }
}
