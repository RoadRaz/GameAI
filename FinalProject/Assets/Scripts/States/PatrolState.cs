using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    // Patrol points defined
    private Vector3[] points = { new Vector3(-7f, 0f, 3.5f), 
                                 new Vector3(-8f, 0f, -5f), 
                                 new Vector3(7.5f, 0f, -4f),
                                 new Vector3(-2f, 0f, -0.5f),
                                 new Vector3(6f, 0f, 3f) };

    private NavMeshAgent nmAgent;

    public PatrolState(EnemyAI enemy) : base(enemy)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("PatrolState");
        enemy.GetComponent<Renderer>().material.color = Color.green;
        nmAgent = enemy.GetComponent<NavMeshAgent>();
        nmAgent.speed = 2f;
        MoveToNext();
        enemy.stateText.text = "Patrol";
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // If it reaches the patrol point
        if (!nmAgent.pathPending && nmAgent.remainingDistance == 0f)
            enemy.ChangeState(new IdleState(enemy));

        // If the player is inside the view range and view angle
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) <= enemy.sightRange)
        {
            if (Vector3.Angle(enemy.transform.forward, enemy.player.transform.position - enemy.transform.position) <= enemy.sightAngle)
                enemy.ChangeState(new ApproachState(enemy));
        }
    }

    public void MoveToNext()
    {
        nmAgent.destination = points[Random.Range(1, 4)];
    }
}
