using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;
using LevelDefinition = System.Collections.Generic.List<UnityEngine.GameObject>;

public class LevelDirector : MonoBehaviour
{
    public static LevelDirector _Self;

    private List<GameObject> EnemyList = new List<GameObject>();
    private GameObject EnemySpawnPoint;
    // List of levels of enemy prefabs to spawn
    public List<LevelDefinition> levels;
    public float CurrentLevel = 0;
    public Text CurrentLevelText;
    public Text EnemiesText;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        levels = new List<LevelDefinition> {
            new LevelDefinition {
                Resources.Load<GameObject>(EnemyPrefab.Enemy1),
                Resources.Load<GameObject>(EnemyPrefab.Enemy1),
                Resources.Load<GameObject>(EnemyPrefab.Enemy1)
            }
        };
        CurrentLevelText.text = "";
        EnemiesText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLevel(int level)
    {
        CurrentLevelText.text = (level + 1).ToString();
        var prefabs = levels[level];
        foreach (GameObject prefab in prefabs)
        {
            var enemy = Instantiate<GameObject>(prefab, EnemySpawnPoint.transform);
            Debug.Log(enemy);
            EnemyList.Add(enemy);
        }
        EnemiesText.text = EnemyList.Count.ToString();
    }

    public void Awake()
    {
        _Self = this;
    }

    public void OnUnitDeath(Unit unit)
    {
        // doesn't work???
        EnemyList.Remove(unit.gameObject);
        EnemiesText.text = EnemyList.Count.ToString();
    }

    public void OnUnitTakeDamage(Unit unit, float damage)
    {
        // TODO score?
    }
}
