    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAIController : MonoBehaviour
{
    public Unit unit;
    public NavMeshAgent agent;

    Unit FindClosestUnit(UnitTeam team)
    {
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        float closestDistance = 0;
        Unit closestUnit = null;
        foreach (Unit u in units)
        {
            if (u.team != team)
                continue;
            float distance = Vector3.Distance(u.transform.position, unit.transform.position);
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
        float distance = Vector3.Distance(target.transform.position, unit.transform.position);

        if (target && distance > 1.5f) // TODO do some ability checks
        {
            Debug.Log(distance);
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
