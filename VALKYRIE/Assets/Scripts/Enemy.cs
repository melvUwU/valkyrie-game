using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public int spawnTime;
    public EnemyType enemyType;
    public int spawner;
    public bool randomSpawn;
    public bool isSpawned;


}
public enum EnemyType
{
    Infrantry,
    Melee,
    Drone,
    Boss
}

