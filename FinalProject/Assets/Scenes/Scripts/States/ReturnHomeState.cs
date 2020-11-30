using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnHomeState : State
{
    private Vector3 destination;
    public ReturnHomeState(EnemyAI enemy) : base(enemy)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.GetComponent<Renderer>().material.color = Color.blue;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        enemy.MoveToward(destination);

        if (ReachedHome())
            enemy.ChangeState(new PatrolState(enemy));
    }

    private bool ReachedHome()
    {
        return Vector3.Distance(enemy.transform.position, destination) < 0.5f;
    }
}
