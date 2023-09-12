using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class ValkyrieController : MonoBehaviour
{
    public int baseMaxHP = 100; // Initial base max health
    public int currentMaxHP;    // Current max health after upgrades
    public int currentHP;       // Current health
    public static bool isDead = false;
    public GameManager gameManager;
    public SkeletonGraphic characterAnimation;
    public AnimationReferenceAsset retreatAnimation;

    private void Start()
    {
        gameManager = GameManager.Instance;
        // Set the initial current max health to the base max health
        currentMaxHP = baseMaxHP;

        // Set the initial health to the current max health
        currentHP = currentMaxHP;

    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        Debug.Log("hurt");
        // Subtract the damage from current health
        currentHP -= damage;

        // Check if the ally's health has dropped to or below zero
        if (currentHP <= 0)
        {
            isDead = true;
            ObjectContainer parentComponent = transform.parent.GetComponent<ObjectContainer>();
            parentComponent.isFull = false;
            if (characterAnimation != null)
            {
                characterAnimation.AnimationState.SetAnimation(0, retreatAnimation, false);
            }
            StartCoroutine(DestroyAfterAnimation());
        }
    }
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(retreatAnimation.Animation.Duration);
        Destroy(gameObject);
    }

    // Method to heal
    public void Heal(int amount)
    {
        // Add the healing amount to current health
        currentHP += amount;

        // Ensure that current health doesn't exceed the current max health
        currentHP = Mathf.Clamp(currentHP, 0, currentMaxHP);
    }

    // Method to upgrade ally's base stats
    public void UpgradeStats(int newMaxHP)
    {
        // Update the current max health to the new value
        currentMaxHP = newMaxHP;

        // Ensure that current health doesn't exceed the new max health
        currentHP = Mathf.Clamp(currentHP, 0, currentMaxHP);
    }
}
