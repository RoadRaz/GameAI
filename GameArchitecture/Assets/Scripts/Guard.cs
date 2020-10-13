using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : GOAPAgent
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SubGoal sg = new SubGoal("GoHome", 1, true);
        goals.Add(sg, 3);
    }
}
