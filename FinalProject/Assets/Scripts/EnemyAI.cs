using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    State currentState;

    public GameObject player;
    public Player playerProps;

    public Transform[] points;

    public float sightRange;
    public float sightAngle;
    public float damage;

    public Text stateText;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new PatrolState(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    public void ChangeState(State state)
    {
        if (currentState != null)
            currentState.OnExit();
        currentState = state;
        // Editor Hierarchy state name
        gameObject.name = "Enemy - " + state.GetType().Name;
        if (currentState != null)
            currentState.OnEnter();
    }
}
