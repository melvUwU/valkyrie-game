using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EnemyController : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    private int wavepointIndex = 0;

    private bool blocked;

    public int maxHP = 100;
    public int currentHP;

    public float attackRange = 1f;

    public SkeletonGraphic enemyAnimation;
    public AnimationReferenceAsset walkAnimation;
    public AnimationReferenceAsset attackAnimation;
    public AnimationReferenceAsset deadAnimation;
    public string currentState;

    public float enemyAtkDelay = 1f;
    public int enemyAtkDamage = 2;

    public float attackTimer;

    private void Start()
    {
        //set default stats of the enemy
        currentHP = maxHP;
        target = Waypoints.points[0];
        blocked = false;
    }

    private void Update()
    {
        //if the enemy isn't blocked, it can walk along the waypoints
        if (!blocked)
        {
            SetCharacterState("Walk");
            Vector2 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        }
        else
        {
            //set the enemy state if its currentHP is greater than 0
            if(currentHP > 0)
            {
                SetCharacterState("Attack");
            }
            
        }
        //if the distance of the waypoint is less than 0.4f, get next waypoint's position
        if (Vector2.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }


    void GetNextWaypoint()
    {
        //if the wavepointIndex is great or equal to waypoint length minus by 1, destroy the enemy game object 
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            ScoreManager.lifePoints--;
            return;
        }
        wavepointIndex++;
        //set next target
        target = Waypoints.points[wavepointIndex];
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

    //set the enemy state and change the animation, delay for 0.3 second then destroy the enemy game object
    IEnumerator Dead()
    {
        SetCharacterState("Dead");
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
        ScoreManager.enemyKill++;
    }

    //initialize enemy attacking system
    public void AttackValkyrie(GameObject valkyrie)
    {
        if (attackTimer <= 0f)
        {
            valkyrie.GetComponent<ValkyrieController>().ValkyrieTakeDamage(enemyAtkDamage);
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

    //initialize block system
    public void SetBlock(bool isBlocked)
    {
        blocked = isBlocked;
    }
}
