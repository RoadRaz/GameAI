using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Office
{
    private static readonly Office instance = new Office();
    private static OfficeStates officeStates;

    static Office()
    {
        officeStates = new OfficeStates();
    }

    private Office() { }

    public static Office GetInstance
    {
        get { return instance; }   
    }

    public OfficeStates GetOffice()
    {
        return officeStates;
    }
}
