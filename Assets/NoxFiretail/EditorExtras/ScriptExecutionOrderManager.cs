using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class ScriptOrderManager
{

    static ScriptOrderManager()
    {
        foreach (MonoScript monoScript in MonoImporter.GetAllRuntimeMonoScripts())
        {
            if (monoScript.GetClass() != null)
            {
                foreach (var a in Attribute.GetCustomAttributes(monoScript.GetClass(), typeof(ScriptExecOrder)))
                {
                    var currentOrder = MonoImporter.GetExecutionOrder(monoScript);
                    var newOrder = ((ScriptExecOrder)a).order;
                    if (currentOrder != newOrder)
                        MonoImporter.SetExecutionOrder(monoScript, newOrder);
                }
            }
        }
    }
}