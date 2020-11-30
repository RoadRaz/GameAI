using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{

    private Vector3 nextDestination;

    private float wanderTime = 5f;

    private float timer;

    public PatrolState(EnemyAI enemy) : base(enemy)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        nextDestination = new Vector3(Random.Range(-40, 40), 0, Random.Range(-40, 40));
        timer = 0f;
        enemy.GetComponent<Renderer>().material.color = Color.green;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(ReachedDestination())
            nextDestination = new Vector3(Random.Range(-40, 40), 0, Random.Range(-40, 40));

        enemy.MoveToward(nextDestination);
        timer += Time.deltaTime;
        if(timer >= wanderTime)
        {
            enemy.ChangeState(new ReturnHomeState(enemy));
        }
    }

    private bool ReachedDestination()
    {
        return Vector3.Distance(enemy.transform.position, nextDestination) < 0.5f;
    }
}
