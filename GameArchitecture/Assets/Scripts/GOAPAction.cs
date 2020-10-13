using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.AI;

public abstract class GOAPAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target;
    public float duration = 0.0f;
    public OfficeState[] preConditions;
    public OfficeState[] afterEffects;
    public NavMeshAgent agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public OfficeStates agentStates;

    public bool running = false;

    public GOAPAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        if(preConditions != null)
        {
            foreach(OfficeState os in preConditions)
            {
                preconditions.Add(os.key, os.value);
            }
        }
        if (afterEffects != null)
        {
            foreach (OfficeState os in afterEffects)
            {
                effects.Add(os.key, os.value);
            }
        }
    }

    public bool isAchievable()
    {
        return true;
    }

    public bool isAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach(KeyValuePair<string,int> p in preconditions)
        {
            if (!conditions.ContainsKey(p.Key))
                return false;
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
