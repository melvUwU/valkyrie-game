using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Drone : MonoBehaviour
{
    public static float speed = 5f;

    public List<Transform> currentWaypoints;
    private int wavepointIndex = 0;
    public string waypointTag;

    public int maxHP = 100;
    public int currentHP;

    public SkeletonGraphic enemyAnimation;
    public AnimationReferenceAsset walkAnimation;
    public AnimationReferenceAsset deadAnimation;
    public AnimationReferenceAsset idleAnimation;
    public string currentState;
    // Start is called before the first frame update
    void Start()
    {
        //set default stats of the enemy
        currentHP = maxHP;
        // Retrieve the waypoints based on the specified tag
        currentWaypoints = new List<Transform>(Waypaths.GetWaypointsForPath(waypointTag));


        if (currentWaypoints.Count > 0)
        {
            SetNextWaypoint();
        }
        else
        {
            Debug.LogError("No waypoints found for path with tag: " + waypointTag);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void Movement()
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

    //set the enemy state and change the animation, delay for 0.3 second then destroy the enemy game object
    IEnumerator Dead()
    {
        SetCharacterState("Dead");
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
        ScoreManager.enemyKill++;
    }
    public void SetAnimation(AnimationReferenceAsset anim, bool loop, float timeScale)
    {
        enemyAnimation.AnimationState.SetAnimation(0, anim, loop).TimeScale = timeScale;
    }

    //set enemy state
    public void SetCharacterState(string state)
    {
        switch (state)
        {
            case "Walk":
                if (currentState == "Walk")
                {
                    break;
                }
                currentState = "Walk";
                SetAnimation(walkAnimation, true, 1f);
                break;
            case "Idle":
                if (currentState == "Idle")
                {
                    break;
                }
                currentState = "Idle";
                SetAnimation(idleAnimation, true, 1f);
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
}
