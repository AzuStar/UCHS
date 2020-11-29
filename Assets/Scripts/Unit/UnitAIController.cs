    using System.Collections;
using System.Collections.Generic;
using NoxRaven;
using UnityEngine;
using UnityEngine.AI;
using static NoxRaven.NoxUnit;

public class UnitAIController : MonoBehaviour
{
    public NoxUnit PossessedUnit;
    public NavMeshAgent Agent;

    NoxUnit FindClosestUnit(UnitTeam team)
    {
        NoxUnit[] units = GameObject.FindObjectsOfType<NoxUnit>();
        float closestDistance = 0;
        NoxUnit closestUnit = null;
        foreach (NoxUnit u in units)
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

        NoxUnit target = FindClosestUnit(UnitTeam.Player);
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
