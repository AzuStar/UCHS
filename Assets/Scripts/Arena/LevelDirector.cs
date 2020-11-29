using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using NoxRaven;
using UnityEngine;
using UnityEngine.UI;
using LevelDefinition = System.Collections.Generic.List<UnityEngine.GameObject>;

public class LevelDirector : MonoBehaviour
{
    public static LevelDirector _Self;

    private List<GameObject> EnemyList = new List<GameObject>();
    private int EnemyListCount = 0;
    private GameObject EnemySpawnPoint;
    // List of levels of enemy prefabs to spawn
    public List<LevelDefinition> levels;
    public int CurrentLevel = 0;
    public Text CurrentLevelText;
    public Button StartLevelButton;
    public Text EnemiesText;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        levels = new List<LevelDefinition> {
            new LevelDefinition {
                Resources.Load<GameObject>(EnemyPrefab.Enemy1),
                Resources.Load<GameObject>(EnemyPrefab.Enemy1),
                Resources.Load<GameObject>(EnemyPrefab.Enemy1),
            },
            new LevelDefinition {
                Resources.Load<GameObject>(EnemyPrefab.Enemy2),
                Resources.Load<GameObject>(EnemyPrefab.Enemy2),
                Resources.Load<GameObject>(EnemyPrefab.Enemy2),
            },
            new LevelDefinition {
                Resources.Load<GameObject>(EnemyPrefab.Enemy1),
                Resources.Load<GameObject>(EnemyPrefab.Enemy1),
                Resources.Load<GameObject>(EnemyPrefab.Enemy1),
                Resources.Load<GameObject>(EnemyPrefab.Enemy2),
                Resources.Load<GameObject>(EnemyPrefab.Enemy2),
                Resources.Load<GameObject>(EnemyPrefab.Enemy2),
            }
        };
        CurrentLevelText.text = "";
        EnemiesText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartNextLevel()
    {
        CurrentLevelText.text = (CurrentLevel + 1).ToString();
        StartLevelButton.interactable = false;
        var prefabs = levels[CurrentLevel];
        foreach (GameObject prefab in prefabs)
        {
            var enemy = Instantiate<GameObject>(prefab, EnemySpawnPoint.transform);
            EnemyList.Add(enemy);
            EnemyListCount++;
        }
        EnemiesText.text = EnemyListCount.ToString();
    }

    public void Awake()
    {
        _Self = this;
    }

    public void OnUnitDeath(NoxUnit unit)
    {
        // doesn't work???
        EnemyList.Remove(unit.gameObject);
        EnemyListCount--;
        // EnemiesText.text = EnemyList.Count.ToString();
        EnemiesText.text = EnemyListCount.ToString();
        if (EnemyListCount <= 0)
        {
            OnLevelCompleted();
        }
    }

    public void OnUnitTakeDamage(NoxUnit unit, float damage)
    {
        // TODO score?
    }

    public void OnLevelCompleted()
    {
        CurrentLevel++;
        CurrentLevelText.text = CurrentLevel.ToString();
        StartLevelButton.interactable = true;
    }
}
