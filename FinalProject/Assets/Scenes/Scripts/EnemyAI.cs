using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    State currentState;

    [SerializeField]
    private float moveSpeed = 1f;

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
        // Console state name
        gameObject.name = "Cube - " + state.GetType().Name;
        if (currentState != null)
            currentState.OnEnter();
    }

    public void MoveToward(Vector3 destination)
    {
        var direction = GetDirection(destination);
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private Vector3 GetDirection(Vector3 destination)
    {
        return (destination - transform.position).normalized;
    }
}
