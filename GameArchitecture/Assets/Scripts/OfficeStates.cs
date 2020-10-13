using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OfficeState
{
    public string key;
    public int value;
}

public class OfficeStates
{
    public Dictionary<string, int> officeStates;

    public OfficeStates()
    {
        officeStates = new Dictionary<string, int>();
    }

    public bool StateExists(string key)
    {
        return officeStates.ContainsKey(key);
    }

    public void AddState(string key, int value)
    {
        officeStates.Add(key, value);
    }

    public void UpdateState(string key, int value)
    {
        if (officeStates.ContainsKey(key))
        {
            officeStates[key] += value;
            if (officeStates[key] <= 0)
                DeleteState(key);
        }
        else
            officeStates.Add(key, value);
    }

    public void DeleteState(string key)
    {
        if (officeStates.ContainsKey(key))
            officeStates.Remove(key);
    }

    public void SetState(string key, int value)
    {
        if (officeStates.ContainsKey(key))
            officeStates[key] = value;
        else
            officeStates.Add(key, value);
    }

    public Dictionary<string, int> GetStates()
    {
        return officeStates;
    }
}
