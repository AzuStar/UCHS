    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAIController : MonoBehaviour
{
    public Unit PossessedUnit;
    public NavMeshAgent Agent;

    Unit FindClosestUnit(UnitTeam team)
    {
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        float closestDistance = 0;
        Unit closestUnit = null;
        foreach (Unit u in units)
        {
            if (u.Team != team)
                continue;
            float distance = Vector3.Distance(u.transform.position, PossessedUnit.transform.position);
            if (!closestUnit || distance < closestDistance)
            {
                closestDistance = distance;
                closestUnit = u;
            }
        }
        return closestUnit;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO select best ability first

        Unit target = FindClosestUnit(UnitTeam.Player);
        float distance = Vector3.Distance(target.transform.position, PossessedUnit.transform.position);

        if (target && distance > 4.0f) // TODO do some ability checks
        {
            Agent.isStopped = false;
            Agent.SetDestination(target.transform.position);
        }
        else
        {
            Agent.isStopped = true;
        }
    }
}
