using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitTeam : ushort
{
    Player = 0,
    Enemy = 1
}

public class Unit : MonoBehaviour
{
    public UnitTeam Team = UnitTeam.Player;
    public float Health = 100;
    public float MaxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
