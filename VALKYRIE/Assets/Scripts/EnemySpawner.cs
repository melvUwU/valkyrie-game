using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public List<Enemy> enemies;
    public GameObject spawner;

    private void Update()
    {
        foreach (Enemy enemy in enemies)
        {
            if(enemy.isSpawned == false && enemy.spawnTime <= Time.time)
            {
                Instantiate(enemyPrefabs[(int)enemy.enemyType], spawner.transform);
                enemy.isSpawned = true;

            }
        }
    }

}

