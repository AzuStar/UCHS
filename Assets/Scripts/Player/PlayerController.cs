using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool MovementAllowed = true;
    public Unit PossessedUnit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            if (Input.GetMouseButtonDown(0))
                if (MovementAllowed)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        PossessedUnit.SetRunning(true);
                        agent.SetDestination(hit.point);
                        agent.isStopped = false;
                    }
                }
        if (agent.remainingDistance < 0.25f)
        {
            PossessedUnit.SetRunning(false);
        }
    }
}
