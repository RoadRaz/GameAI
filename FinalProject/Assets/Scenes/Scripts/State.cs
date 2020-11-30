using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected EnemyAI enemy;

    public State(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    public virtual void OnEnter() { }

    public virtual void UpdateState() { }

    public virtual void OnExit() { }
}
