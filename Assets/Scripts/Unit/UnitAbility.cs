using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbility : MonoBehaviour
{
    public float cooldownTime = 1000.0f;
    private float lastUsedTime;

    // range in meters
    public float range = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Fire()
    {
        lastUsedTime = Time.time;
    }

    bool CanFire()
    {
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        foreach (Unit unit in units) {
            Debug.Log(unit);

        }
        return false;
    }
}
