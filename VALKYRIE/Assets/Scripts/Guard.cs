using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Guard : MonoBehaviour
{
    public List<GameObject> blockedEnemies = new List<GameObject>();

    public float blockRange = 1f;
    public int maxBlockedEnemies = 1;

    public SkeletonGraphic valkyrieAnimation;
    public AnimationReferenceAsset idleAnimation;
    public AnimationReferenceAsset attackingAnimation;
    public string currentState;

    public float attackDelay = 1f;
    public int attackDamage = 10;

    public float attackTimer;

    [SerializeField]
    public int deployCost;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        //set the current state of the character
        SetCharacterState("Idle");
    }

    private void Update()
    {
        //initialize the enemies list with game object that contains tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //set number of blocked enemies to default
        int numBlockedEnemies = 0;

        foreach (GameObject enemy in enemies)
        {
            //if the enemy magnitude is less or eqaul to block range, block that enemy
            if (IsCollidingWithEnemy(enemy) && !blockedEnemies.Contains(enemy))
            {
                if (blockedEnemies.Count < maxBlockedEnemies)
                {
                    blockedEnemies.Add(enemy);
                    enemy.GetComponent<EnemyController>().SetBlock(true);
                    SetCharacterState("Attack");
                    currentState = "Attack";
                    Attack(enemy);
                    blockedEnemies.RemoveAt(numBlockedEnemies);
                }
            }
            else
            {
                enemy.GetComponent<EnemyController>().SetBlock(false);
            }
            blockedEnemies.RemoveAll(enemy => !IsCollidingWithEnemy(enemy));
        }

        // After checking all enemies, set the state to "Idle" if no enemies are blocking
        if (blockedEnemies.Count == 0)
        {
            SetCharacterState("Idle");
            currentState = "Idle";
        }
    }

    //initialize attack system
    private void Attack(GameObject enemy)
    {
        if (attackTimer <= 0f)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
            attackTimer = attackDelay;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }
    //set the animation of the character
    public void SetAnimation(AnimationReferenceAsset anim, bool loop, float timeScale)
    {
        valkyrieAnimation.AnimationState.SetAnimation(0, anim, loop).TimeScale = timeScale;
    }
    //set the character state
    public void SetCharacterState(string state)
    {
        switch (state)
        {
            case "Idle":
                if(currentState == "Idle")
                {
                    break;
                }
                SetAnimation(idleAnimation, true, 1f);
                currentState = "Idle";
                Debug.Log("Set to idle");
                break;
            case "Attack":
                if (currentState == "Attack")
                {
                    break;
                }
                SetAnimation(attackingAnimation, true, 1f);
                currentState = "Attack";
                Debug.Log("Set to Attack");
                break;
        }
    }

    private bool IsCollidingWithEnemy(GameObject enemy)
    {
        Collider2D guardCollider = GetComponent<Collider2D>();
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();

        return guardCollider.bounds.Intersects(enemyCollider.bounds);

    }
}
