using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitTeam : ushort
{
    Player = 0,
    Enemy = 1
}

public static class EnemyPrefab
{
    public static String Enemy1 { get { return "Unit/Enemy1Prefab"; } }
    public static String Enemy2 { get { return "Unit/Enemy2Prefab"; } }
    public static String Enemy3 { get { return "Unit/Enemy3Prefab"; } }
    public static String Enemy4 { get { return "Unit/Enemy4Prefab"; } }
    public static String Enemy5 { get { return "Unit/Enemy5Prefab"; } }
}

public class Unit : MonoBehaviour
{
    public UnitTeam Team = UnitTeam.Player;
    private float Health;
    public float MaxHealth = 100;
    public UnitCanvasController CanvasController;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
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
            LevelDirector._Self.OnUnitDeath(source);
            Destroy(gameObject);
        }
        else
        {
            CanvasController.OnHealthChanged(Health, MaxHealth);
        }
    }
}
