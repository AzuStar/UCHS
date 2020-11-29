using System.Collections;
using System.Collections.Generic;
using NoxRaven;
using UnityEngine;

public class UnitAbility : MonoBehaviour
{
    public float CooldownTime = 1000.0f;
    private float LastUsedTime;

    // range in meters
    public float Range = 2.0f;

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
        LastUsedTime = Time.time;
    }

    bool CanFire()
    {
        NoxUnit[] units = GameObject.FindObjectsOfType<NoxUnit>();
        foreach (NoxUnit unit in units) {
            // Debug.Log(unit);
        }
        return false;
    }
}
