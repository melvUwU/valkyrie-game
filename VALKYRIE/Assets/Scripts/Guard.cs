using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Guard : MonoBehaviour
{
    public float blockRange = 1f;
    public int maxBlockedEnemies = 1;

    public SkeletonGraphic valkyrieAnimation;
    public AnimationReferenceAsset idleAnimation;
    public AnimationReferenceAsset attackingAnimation;
    public string currentState;

    public float attackDelay = 1f;
    public int attackDamage = 10;

    public float attackTimer;

    private void Start()
    {
        //set the current state of the character
        currentState = "Idle";
        SetCharacterState(currentState);
    }

    private void Update()
    {
        //initialize the enemies list with game object that contains tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //set number of blocked enemies to default
        int numBlockedEnemies = 0;
        foreach (GameObject enemy in enemies)
        {
            Vector3 direction = enemy.transform.position - transform.position;

            //if the enemy magnitude is less or eqaul to block range, block that enemy
            if (direction.magnitude <= blockRange)
            {
                Debug.Log("Block");
                numBlockedEnemies++;
                //if number of blocked enemies is less or eqaul to max blockeed enemies, attack that enemy
                if (numBlockedEnemies <= maxBlockedEnemies)
                {
                    enemy.GetComponent<EnemyController>().SetBlock(true);
                    Attack(enemy);
                }
            }
            else
            {
                enemy.GetComponent<EnemyController>().SetBlock(false);
            }
        }
        //if number of blocked enemy is greater than 0, set the character state to "Attack"
        if (numBlockedEnemies > 0)
        {
            SetCharacterState("Attack");
        }
        else
        {
            SetCharacterState("Idle");
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
                break;
            case "Attack":
                if (currentState == "Attack")
                {
                    break;
                }
                SetAnimation(attackingAnimation, true, 1f);
                currentState = "Attack";
                break;
        }
    }
}
