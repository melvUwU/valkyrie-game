using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public List<GameObject> blockedEnemies = new List<GameObject>();
    public List<GameObject> healedAllies = new List<GameObject>();
    public int maxBlockedEnemies; // Changed from private to protected

    protected bool IsCollidingWithEnemy(GameObject enemy)
    {
        Collider2D characterCollider = GetComponent<Collider2D>();
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();

        return characterCollider.bounds.Intersects(enemyCollider.bounds);
    }
}
