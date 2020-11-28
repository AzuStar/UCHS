using System;
using System.Collections.Generic;
using UnityEngine;

public class ScriptExecOrder : Attribute
{
    public int order;

    public ScriptExecOrder(int order)
    {
        this.order = order;
    }
}