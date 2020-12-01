using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ApproachState : State
{
    private NavMeshAgent nmAgent;
    public ApproachState(EnemyAI enemy) : base(enemy)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("ApproachState");
        enemy.GetComponent<Renderer>().material.color = Color.red;
        nmAgent = enemy.GetComponent<NavMeshAgent>();
        nmAgent.speed = 6f;
        enemy.stateText.text = "Approach";
    }

    public override void UpdateState()
    {
        base.UpdateState();
        nmAgent.destination = enemy.player.transform.position - (enemy.transform.forward * 1f);
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) <= 1.5f)
        {
            enemy.ChangeState(new AttackState(enemy));
        }
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) >= enemy.sightRange)
        {
            Vector3 searchTrail = enemy.transform.position - enemy.player.transform.position;
            nmAgent.destination = searchTrail;
            enemy.ChangeState(new PatrolState(enemy));
        }

    }
}
