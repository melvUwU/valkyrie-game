using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Boss : MonoBehaviour
{
    public GameObject attackWith;
    public GameObject[] valkyries;

    [SerializeField]
    public static float speed = 4f;

    public List<Transform> currentWaypoints;
    private int wavepointIndex = 0;

    public bool blocked;
    public int maxBlockedValk = 1;

    public int maxHP = 100;
    public int currentHP;

    public float attackRange = 1f;

    public SkeletonGraphic enemyAnimation;
    public AnimationReferenceAsset walkAnimation;
    public AnimationReferenceAsset attackAnimation;
    public AnimationReferenceAsset deadAnimation;
    public AnimationReferenceAsset idleAnimation;
    public string currentState;

    public float enemyAtkDelay = 3f;
    public int enemyAtkDamage = 10;

    public float attackTimer;

    public string waypointTag; // Tag to determine the path

    private bool initialMovementCompleted = false;
    private bool canBeAttacked = false;

    private void Start()
    {
        // Set default stats of the enemy
        currentHP = maxHP;
        blocked = false;

        // Retrieve the waypoints based on the specified tag
        currentWaypoints = new List<Transform>(Waypaths.GetWaypointsForPath(waypointTag));

        if (currentWaypoints.Count > 0)
        {
            // Start the initial behavior coroutine
            StartCoroutine(InitialBehavior());
        }
        else
        {
            Debug.LogError("No waypoints found for path with tag: " + waypointTag);
            Destroy(gameObject);
        }
    }

    private IEnumerator InitialBehavior()
    {
        // Boss walks for the first 5 seconds
        yield return new WaitForSeconds(5f);
        initialMovementCompleted = true;
        Debug.Log(initialMovementCompleted);

        // Boss stays idle for 30 seconds and can't be attacked
        SetCharacterState("Idle");
        canBeAttacked = false;
        yield return new WaitForSeconds(30f);

        // Boss walks to the next waypoint for 10 seconds
        SetNextWaypoint();
        yield return new WaitForSeconds(10f);

        // Boss stays idle for 20 seconds and can be attacked
        SetCharacterState("Idle");
        canBeAttacked = true;
        yield return new WaitForSeconds(20f);

        // Boss walks to the next waypoint
        SetNextWaypoint();
        enemyAtkDamage = enemyAtkDamage / 100;
    }

    private void Update()
    {
        if (initialMovementCompleted && !blocked)
        {
            Movement();
        }

        if (attackWith == null)
        {
            SetBlock(false);
        }

        valkyries = GameObject.FindGameObjectsWithTag("Valkyrie");

        foreach (GameObject valkyrie in valkyries)
        {
            CharacterBase character = valkyrie.GetComponent<CharacterBase>();

            if (IsCollidingWithEnemy(valkyrie) && character != null && canBeAttacked)
            {
                if (character.blockedEnemies.Count < character.maxBlockedEnemies || character.blockedEnemies.Contains(gameObject))
                {
                    enemyAtkDamage *= 20;
                    attackWith = valkyrie;
                    SetCharacterState("Attack");
                    AttackValkyrie(valkyrie);
                    SetBlock(true);
                }
            }
        }
    }

    void SetNextWaypoint()
    {
        // Increment the waypoint index
        wavepointIndex++;

        // If the enemy reached the end of the path, destroy it
        if (wavepointIndex >= currentWaypoints.Count)
        {
            Destroy(gameObject);
            ScoreManager.lifePoints--;
        }
    }


    //set the damage taken when the enemy is attacked
    public void TakeDamage(int damage)
    {
        //minus the currentHP equals to the damage taken
        currentHP -= damage;
        //if the currentHP is less or equal to 0, start Dead coroutine
        if (currentHP <= 0)
        {
            StartCoroutine(Dead());
        }
    }

    public void Movement()
    {
        // If the enemy isn't blocked, it can walk along the waypoints
        if (!blocked)
        {
            SetCharacterState("Walk");
            Vector2 dir = currentWaypoints[wavepointIndex].position - transform.position;

            // Calculate the angle between the current position and the next waypoint's position
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Check if the angle is greater than 90 degrees or less than -90 degrees (indicating a leftward movement)
            bool isMovingLeft = angle > 90f || angle < -90f;

            // Set the ScaleX property to -1 when moving left to flip the sprite horizontally
            enemyAnimation.Skeleton.ScaleX = isMovingLeft ? -1f : 1f;

            // Reset the rotation on the X-axis to zero
            transform.eulerAngles = new Vector3(0, 0, 0);

            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            // Check if the enemy reached the current waypoint
            if (Vector2.Distance(transform.position, currentWaypoints[wavepointIndex].position) <= 0.4f)
            {
                SetNextWaypoint();
            }
        }
        else
        {
            // Set the enemy state if its currentHP is greater than 0
            if (currentHP > 0)
            {
                SetCharacterState("Attack");
            }
        }

    }

    //set the enemy state and change the animation, delay for 0.3 second then destroy the enemy game object
    public IEnumerator Dead()
    {
        SetCharacterState("Dead");
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
        ScoreManager.enemyKill++;
    }

    //initialize enemy attacking system
    public void AttackValkyrie(GameObject valkyrie)
    {
        if (attackTimer <= 0f)
        {
            ValkyrieController valkyrieController = valkyrie.GetComponent<ValkyrieController>();
            Debug.Log("attack");
            if (valkyrieController != null)
            {
                valkyrieController.TakeDamage(enemyAtkDamage);
            }
            attackTimer = enemyAtkDelay;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    //set enemy animation
    public void SetAnimation(AnimationReferenceAsset anim, bool loop, float timeScale)
    {
        enemyAnimation.AnimationState.SetAnimation(0, anim, loop).TimeScale = timeScale;
    }

    //set enemy state
    public void SetCharacterState(string state)
    {
        switch (state)
        {
            case "Idle":
                if (currentState == "Idle")
                {
                    break;
                }
                currentState = "Idle";
                SetAnimation(walkAnimation, true, 1f);
                break;
            case "Walk":
                if (currentState == "Walk")
                {
                    break;
                }
                currentState = "Walk";
                SetAnimation(walkAnimation, true, 1f);
                break;
            case "Attack":
                if (currentState == "Attack")
                {
                    break;
                }
                currentState = "Attack";
                SetAnimation(attackAnimation, true, 1f);
                break;
            case "Dead":
                if (currentState == "Dead")
                {
                    break;
                }
                currentState = "Dead";
                SetAnimation(deadAnimation, false, 1f);
                break;
        }
    }
    private bool IsCollidingWithEnemy(GameObject valk)
    {
        Collider2D guardCollider = GetComponent<Collider2D>();
        Collider2D enemyCollider = valk.GetComponent<Collider2D>();

        return guardCollider.bounds.Intersects(enemyCollider.bounds);

    }
    //initialize block system
    public void SetBlock(bool isBlocked)
    {
        blocked = isBlocked;
    }
}
