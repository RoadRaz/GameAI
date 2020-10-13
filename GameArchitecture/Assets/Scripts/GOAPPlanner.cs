using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> states;
    public GOAPAction action;

    public Node(Node parent, float cost, Dictionary<string, int> states, GOAPAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.states = new Dictionary<string, int>(states);
        this.action = action;
    }
}

public class GOAPPlanner
{
    public Queue<GOAPAction> plan(List<GOAPAction> actions, Dictionary<string,int> goal, OfficeStates states)
    {
        List<GOAPAction> usableActions = new List<GOAPAction>();
        foreach(GOAPAction a in actions)
        {
            if (a.isAchievable())
                usableActions.Add(a);
        }

        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0.0f, Office.GetInstance.GetOffice().GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if(!success)
        {
            Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = null;
        foreach(Node leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                if (leaf.cost < cheapest.cost)
                    cheapest = leaf;
            }
        }

        List<GOAPAction> result = new List<GOAPAction>();
        Node n = cheapest;
        while(n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GOAPAction> queue = new Queue<GOAPAction>();
        foreach(GOAPAction a in result)
        {
            queue.Enqueue(a);
        }

        Debug.Log("The Plan is: ");
        foreach(GOAPAction a in queue)
        {
            Debug.Log("Q: " + a.actionName);
        }

        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GOAPAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        foreach(GOAPAction act in usableActions)
        {
            if(act.isAchievableGiven(parent.states))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.states);
                foreach(KeyValuePair<string, int> eff in act.effects)
                {
                    if (!currentState.ContainsKey(eff.Key))
                        currentState.Add(eff.Key, eff.Value);
                }

                Node node = new Node(parent, parent.cost + act.cost, currentState, act);

                if(GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GOAPAction> subset = ActionSubset(usableActions, act);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                        foundPath = true;
                }
            }
        }
        return foundPath;
    }

    private List<GOAPAction> ActionSubset(List<GOAPAction> actions, GOAPAction actionToRemove)
    {
        List<GOAPAction> subset = new List<GOAPAction>();
        foreach(GOAPAction act in actions)
        {
            if (!act.Equals(actionToRemove))
                subset.Add(act);
        }
        return subset;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach (KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
                return false;
        }
        return true;
    }
}
