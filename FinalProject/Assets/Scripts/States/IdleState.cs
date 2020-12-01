using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private float timer;
    private float idleTime = 3f;
    public IdleState(EnemyAI enemy) : base(enemy)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("IdleState");
        enemy.GetComponent<Renderer>().material.color = Color.blue;
        timer = 0f;
        enemy.stateText.text = "Idle";
    }

    public override void UpdateState()
    {
        base.UpdateState();
        timer += Time.deltaTime;

        // If the player is inside the view range and view angle
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) <= enemy.sightRange)
        {
            if (Vector3.Angle(enemy.transform.forward, enemy.player.transform.position - enemy.transform.position) <= enemy.sightAngle)
                enemy.ChangeState(new ApproachState(enemy));
        }

        // If the player waits for certain time, start patrolling again
        if (timer >= idleTime)
            enemy.ChangeState(new PatrolState(enemy));
    }
}
