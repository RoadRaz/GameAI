using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    private NavMeshAgent nmAgent;
    public AttackState(EnemyAI enemy) : base(enemy)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("AttackState");
        enemy.GetComponent<Renderer>().material.color = Color.black;
        enemy.stateText.text = "Attack";
    }

    public override void UpdateState()
    {
        base.UpdateState();

        // Keep looking at player if player is at any position nearby
        Vector3 lookPlayerLock = new Vector3(enemy.player.transform.position.x, enemy.transform.position.y, enemy.player.transform.position.z);
        enemy.transform.LookAt(lookPlayerLock);

        // Deal damage to player
        enemy.playerProps.health -= enemy.damage * Time.deltaTime;
        enemy.playerProps.healthBar.value = enemy.playerProps.health/enemy.playerProps.maxHealth;

        // if the player is inside the view state but not very close, go to approach state
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) > 1.5f)
            if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) <= enemy.sightRange)
                enemy.ChangeState(new ApproachState(enemy));
    }
}
