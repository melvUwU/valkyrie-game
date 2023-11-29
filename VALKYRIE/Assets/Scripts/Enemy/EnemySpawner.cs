using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public List<Enemy> enemies;
    public GameObject spawner;
    public int SpawnOnEP;
    public string currentScene;

    private void Update()
    {
        int currentEP = CurrentEP();

        foreach (Enemy enemy in enemies)
        {
            if(currentEP==SpawnOnEP && enemy.isSpawned == false && enemy.spawnTime <= Time.time)
            {
                Instantiate(enemyPrefabs[(int)enemy.enemyType], spawner.transform).SetActive(true);
                enemy.isSpawned = true;

            }
        }
    }

    private int CurrentEP()
    {
        currentScene = SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "Gamplay Part 1":
                return 1;
            case "Gamplay Part 2":
                return 2;
            case "Gamplay Part 3":
                return 3;
            case "Gamplay Part 4":
                return 4;
            case "Gamplay Part 5":
                return 5;
            default: return 0;
        }
    }

}

