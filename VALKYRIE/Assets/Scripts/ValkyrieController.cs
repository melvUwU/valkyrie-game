using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValkyrieController : MonoBehaviour
{
    public int maxValHP = 100;
    public int currentValHP;

    public Image hpBar;
    // Start is called before the first frame update
    void Start()
    {
        hpBar = hpBar.GetComponent<Image>();
        currentValHP = maxValHP;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = currentValHP;
    }
    public void ValkyrieTakeDamage(int enemyDamage)
    {
        currentValHP -= enemyDamage;
        if (currentValHP < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
