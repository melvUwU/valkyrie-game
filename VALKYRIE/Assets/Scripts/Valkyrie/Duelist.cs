using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Duelist : CharacterBase
{

    public float blockRange = 1f;

    public SkeletonGraphic valkyrieAnimation;
    public AnimationReferenceAsset deployAnimation;
    public AnimationReferenceAsset retreatAnimation;
    public AnimationReferenceAsset idleAnimation;
    public AnimationReferenceAsset attackingAnimation;
    public string currentState;

    public float attackDelay = 1f;
    public int attackDamage = 50;

    public float attackTimer;

    [SerializeField]
    public int deployCost;
    private GameManager gameManager;

    private void Start()
    {
        maxBlockedEnemies = 1;
        if (ObjectCard.isDeployed == true)
        {
            SetCharacterState("Deploy");
            StartCoroutine(WaitForAnimation(deployAnimation, "Idle"));
        }
        else
        {
            // Set the current state of the character to "Idle"
            SetCharacterState("Idle");
        }
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
    public void SetCharacterState(string state)
    {
        if(currentState == state)
        {
            return;
        }
        switch (state)
        {
            case "Idle":
                if (currentState == "Idle")
                {
                    currentState = "Idle";
                    break;
                }
                SetAnimation(idleAnimation, true, 1f);
                break;
            case "Attack":
                if (currentState == "Attack")
                {
                    currentState = "Attack";
                    break;
                }
                SetAnimation(attackingAnimation, true, 1f);
                break;
            case "Deploy":
                if (currentState == "Deploy")
                {
                    currentState = "Deploy";
                    break;
                }
                SetAnimation(deployAnimation, true, 1f);
                break;
            case "Dead":
                if (currentState == "Dead")
                {
                    currentState = "Dead";
                    break;
                }
                SetAnimation(retreatAnimation, true, 1f);
                break;
        }
        currentState = state;
    }

    private IEnumerator WaitForAnimation(AnimationReferenceAsset animation, string nextState)
    {
        // Play the specified animation
        SetAnimation(animation, true, 1f);

        // Wait for the animation to finish
        yield return new WaitForSeconds(animation.Animation.Duration);

        // Transition to the next state after the animation
        SetCharacterState(nextState);
    }
}
