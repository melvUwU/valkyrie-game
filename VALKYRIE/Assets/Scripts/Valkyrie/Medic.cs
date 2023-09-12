using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Medic : CharacterBase
{
    public float healRange = 200f; // Range for healing
    public int maxHealedAllies = 1;

    public SkeletonGraphic medicAnimation;
    public AnimationReferenceAsset deployAnimation;
    public AnimationReferenceAsset retreatAnimation;
    public AnimationReferenceAsset idleAnimation;
    public AnimationReferenceAsset healingAnimation;
    public string currentState;

    public GameObject healPrefab; // Prefab for bullets
    public Transform healPoint; // Point where bullets will be fired from

    public float healDelay = 1f;
    private float healTimer = 0f;
    public int healAmount = 30;

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
        // Find all allies in the scene with the "Ally" tag
        GameObject[] valkyries = GameObject.FindGameObjectsWithTag("Valkyrie");

        // Clear the list of healed allies
        healedAllies.RemoveAll(element => !element);

        foreach (GameObject valk in valkyries)
        {
            // Calculate the distance to the ally
            float distanceToAlly = Vector2.Distance(transform.position, valk.transform.position);

            // Check if the ally is within healing range and needs healing
            if (distanceToAlly <= healRange && valk.GetComponent<ValkyrieController>().currentHP < valk.GetComponent<ValkyrieController>().baseMaxHP)
            {
                if(healedAllies.Count < maxHealedAllies)
                {
                    if (!healedAllies.Contains(valk))
                    {
                        healedAllies.Add(valk);
                    }
                    //SetCharacterState("Heal");
                }
                if (healedAllies.Contains(valk) && valk.GetComponent<ValkyrieController>().currentHP < valk.GetComponent<ValkyrieController>().baseMaxHP)
                {

                        Debug.Log("heal");
                        Heal(valk);
                        SetCharacterState("Heal");
                }
                else
                {
                    SetCharacterState("Idle");
                }
            }
        }

    }

    private void Heal(GameObject ally)
    {
        if (healTimer <= 0f)
        {
            // Heal the ally
            ally.GetComponent<ValkyrieController>().Heal(healAmount);
            healTimer = healDelay;
            //healedAllies.Add(ally);
        }
        else
        {
            healTimer -= Time.deltaTime;
        }
    }

    public void SetAnimation(AnimationReferenceAsset anim, bool loop, float timeScale)
    {
        medicAnimation.AnimationState.SetAnimation(0, anim, loop).TimeScale = timeScale;
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
                if(currentState == "Idle")
                {
                    currentState = "Idle";
                    break;
                }
                SetAnimation(idleAnimation, true, 1f);
                break;
            case "Heal":
                if (currentState == "Heal")
                {
                    currentState = "Heal";
                    break;
                }
                SetAnimation(healingAnimation, true, 1f);
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
