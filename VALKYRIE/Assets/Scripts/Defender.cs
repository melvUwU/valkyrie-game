using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Defender : MonoBehaviour
{
    public List<GameObject> blockedEnemies = new List<GameObject>();

    public float blockRange = 100f;
    public int maxBlockedEnemies = 3;

    public SkeletonGraphic valkyrieAnimation;
    public AnimationReferenceAsset deployAnimation;
    public AnimationReferenceAsset retreatAnimation;
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
        int numBlockedEnemies = 0;
        foreach (GameObject enemy in enemies)
        {
            //if the enemy magnitude is less or eqaul to block range, block that enemy
            if (IsCollidingWithEnemy(enemy) && !blockedEnemies.Contains(enemy))
            {
                Debug.Log("block");
                if (blockedEnemies.Count < maxBlockedEnemies)
                {
                    blockedEnemies.Add(enemy);
                    enemy.GetComponent<EnemyController>().SetBlock(true);
                    Debug.Log(blockedEnemies.Count);
                    Attack(enemy);
                    SetCharacterState("Attack");
                    blockedEnemies.RemoveAt(numBlockedEnemies);
                    Debug.Log(blockedEnemies.Count);
                }
            }
            else
            {
                enemy.GetComponent<EnemyController>().SetBlock(false);
                SetCharacterState("Idle");
            }
            blockedEnemies.RemoveAll(enemy => !IsCollidingWithEnemy(enemy));
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
    public void SetCharacterState(string state)
    {
        if (currentState == state)
        {
            return; // Do nothing if the state is already set
        }

        switch (state)
        {
            case "Idle":
                SetAnimation(idleAnimation, true, 1f);
                break;
            case "Attack":
                SetAnimation(attackingAnimation, true, 1f);
                break;
            case "Deploy":
                SetAnimation(deployAnimation, true, 1f);
                break;
            case "Dead":
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

    private bool IsCollidingWithEnemy(GameObject enemy)
    {
        Collider2D guardCollider = GetComponent<Collider2D>();
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();

        return guardCollider.bounds.Intersects(enemyCollider.bounds);

    }
}
