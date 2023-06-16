using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * -has health. 
 * Things can request to apply damage to damageable's health
 * If health hits 0, damageable dies. 
*/

public class Damageable : MonoBehaviour
{
    int health = 3;
    
    public void ApplyDamageRequest(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

}
