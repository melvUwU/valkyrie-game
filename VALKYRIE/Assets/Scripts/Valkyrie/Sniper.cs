using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Sniper : CharacterBase
{
    public float blockRange = 100f;
    public float attackRange = 200f; // Range for attacking

    public SkeletonGraphic valkyrieAnimation;
    public AnimationReferenceAsset deployAnimation;
    public AnimationReferenceAsset retreatAnimation;
    public AnimationReferenceAsset idleAnimation;
    public AnimationReferenceAsset attackingAnimation;
    public string currentState;

    public float attackDelay = 1f;
    private float attackTimer = 0f;
    public int attackDamage = 10;

    public GameObject bulletPrefab; // Prefab for bullets
    public Transform firePoint; // Point where bullets will be fired from

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
        // Find all enemies in the scene with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Clear the list of blocked enemies
        blockedEnemies.Clear();

        foreach (GameObject enemy in enemies)
        {
            // Calculate the distance to the enemy
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            // Check if the enemy is within attack range
            if (distanceToEnemy <= attackRange)
            {
                if (blockedEnemies.Count < maxBlockedEnemies)
                {
                    if (!blockedEnemies.Contains(enemy))
                    {
                        blockedEnemies.Add(enemy);
                    }
                    SetCharacterState("Attack");
                }
            }

            // Check if the enemy is within blocking range
            if (distanceToEnemy <= blockRange)
            {
                if (blockedEnemies.Contains(enemy))
                {
                    Attack(enemy);
                    SetCharacterState("Attack");
                }
            }
        }

        // Unblock enemies that are not in the blockedEnemies list
        //UnblockEnemies();

        // Set the character state based on whether there are blocked enemies
        if (blockedEnemies.Count == 0)
        {
            SetCharacterState("Idle");
        }
    }

    //private void UnblockEnemies()
    //{
    //    // Loop through all enemies in the scene
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //    foreach (GameObject enemy in enemies)
    //    {
    //        if (!blockedEnemies.Contains(enemy))
    //        {
    //            enemy.GetComponent<EnemyMovement>().SetBlock(false);
    //        }
    //    }
    //}

    private void Attack(GameObject enemy)
    {
        if (attackTimer <= 0f)
        {
            // Create a bullet and fire it towards the enemy
            FireBullet(enemy.transform.position);
            Debug.Log("attack");

            // Deal damage to the enemy
            enemy.GetComponent<EnemyMovement>().TakeDamage(attackDamage);
            attackTimer = attackDelay;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void FireBullet(Vector3 targetPosition)
    {
        // Instantiate a bullet at the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Calculate the direction to the target
        Vector3 direction = (targetPosition - firePoint.position).normalized;

        // Rotate the bullet to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Destroy the bullet after a delay (adjust this as needed)
        Destroy(bullet, 1f);
    }

    public void SetAnimation(AnimationReferenceAsset anim, bool loop, float timeScale)
    {
        valkyrieAnimation.AnimationState.SetAnimation(0, anim, loop).TimeScale = timeScale;
    }

    public void SetCharacterState(string state)
    {
        if (currentState == state)
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
