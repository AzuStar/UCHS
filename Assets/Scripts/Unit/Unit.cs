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
    public UnitCanvasController CanvasController;
    private bool IsAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        CanvasController.OnHealthChanged(Health, MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DealDamage(Unit target, float dmg)
    {
        target.TakeDamage(this, dmg);
    }

    public void TakeDamage(Unit source, float dmg)
    {
        if ((Health -= dmg) <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            CanvasController.OnHealthChanged(Health, MaxHealth);
        }
    }
}
