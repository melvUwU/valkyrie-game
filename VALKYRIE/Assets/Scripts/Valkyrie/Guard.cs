using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Guard : CharacterBase
{

    public float blockRange = 1f;

    public SkeletonGraphic valkyrieAnimation;
    public AnimationReferenceAsset idleAnimation;
    public AnimationReferenceAsset attackingAnimation;
    public string currentState;

    public GameObject attackEnemy;

    public float attackDelay = 1f;
    public int attackDamage = 10;

    public float attackTimer;

    [SerializeField]
    public int deployCost;
    private GameManager gameManager;

    private void Start()
    {
        maxBlockedEnemies = 1;
        gameManager = GameManager.Instance;
        //set the current state of the character
        SetCharacterState("Idle");
    }

    private void Update()
    {
        //initialize the enemies list with game object that contains tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //set number of blocked enemies to default
        blockedEnemies.RemoveAll(element => !element);

        foreach (GameObject enemy in enemies)
        {
            //if the enemy magnitude is less or eqaul to block range, block that enemy
            if (IsCollidingWithEnemy(enemy))
            {
                if (blockedEnemies.Count < maxBlockedEnemies)
                {
                    if (!blockedEnemies.Contains(enemy))
                    {
                        blockedEnemies.Add(enemy);
                    }
                    SetCharacterState("Attack");
                }
                if (blockedEnemies.Contains(enemy))
                {
                    Attack(enemy);
                    SetCharacterState("Attack");
                }
            }
        }

        // After checking all enemies, set the state to "Idle" if no enemies are blocking
        if (blockedEnemies.Count == 0)
        {
            SetCharacterState("Idle");
        }
    }

    //initialize attack system
    private void Attack(GameObject enemy)
    {
        if (attackTimer <= 0f)
        {
            enemy.GetComponent<EnemyMovement>().TakeDamage(attackDamage);
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
                
                if (currentState == "Idle")
                {
                    currentState = "Idle";
                    break;
                }
                currentState = "Idle";
                SetAnimation(idleAnimation, true, 1f);
                Debug.Log("Set to idle");
                break;
            case "Attack":
                if (currentState == "Attack")
                {
                    currentState = "Attack";
                    break;
                }
                currentState = "Attack";
                SetAnimation(attackingAnimation, true, 1f);
                Debug.Log("Set to Attack");
                break;
        }
    }
}
