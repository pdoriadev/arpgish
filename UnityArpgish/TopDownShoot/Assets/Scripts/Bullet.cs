using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Bullet class and prefab
- bullet moves in direction it is facing every physics tick
- bullet dies when 
	- collides with something
	- die timer reaches 0
- applies damage to damageable on collision with damageable
*/

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private float speed = 6;
    private float deathTimerStartValue = 1;
    private float deathTimer;
    Rigidbody rbody;

    void Awake()
    {
        rbody = gameObject.GetComponent<Rigidbody>();
        if (rbody == null)
            Debug.LogError("No rigidbody on bullet");
    }

    private void Start()
    {
        deathTimer = deathTimerStartValue;
    }

    private void FixedUpdate()
    {
        deathTimer -= Time.fixedDeltaTime;
        if (deathTimer <= 0)
        {
            Die();
        }
        rbody.AddForce(gameObject.transform.forward * speed, ForceMode.Impulse);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Damageable dam = collision.gameObject.GetComponent<Damageable>();
        if (dam != null)
        {
            dam.ApplyDamageRequest(1);
        }
        Die();
    }

}
