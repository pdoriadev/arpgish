using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct attackData
{
    public float attackSpeed, minAttackRange, maxAttackRange, timeLastAttackAttempted, timeLastAttackEnded;
    public int attackDamage;
    public bool isAttacking;

    public attackData(float _speed, float _minAttackRange, float _maxAttackRange, 
            int _damage, bool _isAttacking, float _timeLastAttackAttempted, float _timeSinceAttackEnded)
    {
        attackSpeed = _speed;
        minAttackRange = _minAttackRange;
        maxAttackRange = _maxAttackRange;
        attackDamage = _damage;
        isAttacking = _isAttacking;
        timeLastAttackAttempted = _timeLastAttackAttempted;
        timeLastAttackEnded = _timeSinceAttackEnded;
    }
}


public class Attacker : MonoBehaviour
{
    [SerializeField]
    int attackDamage = 1;
    [SerializeField]
    float attackSpeed = 1f, closestMeleeRange = 1.5f, maxMeleeRange = 2f, attackTime = 0.3f;

    bool isAttacking = false;
    float timeLastAttackAttempted = 0f, timeLastAttackEnded = 0f;

    [SerializeField]
    GameObject attackBall;
    
    public attackData getAttackData()
    {
        return new attackData(attackSpeed, closestMeleeRange, maxMeleeRange, attackDamage, isAttacking, timeLastAttackAttempted, timeLastAttackEnded);
    }

    public void RequestAttack()
    {
        if (isAttacking == false)
        {
            StartCoroutine(AttackCo());
        }
    }

    public void RequestStopAttack()
    {
        if (isAttacking)
        {
            StopCoroutine(AttackCo());
            StopAttack();
        }
    }

    IEnumerator AttackCo()
    {
        Attack();
        yield return new WaitForSeconds(1 / attackTime);
        StopAttack();
        yield return new WaitForSeconds(1 / attackSpeed);
    }

    void Attack()
    {
        isAttacking = true;
        attackBall.SetActive(true);
        timeLastAttackAttempted = Time.time;
    }

    void StopAttack()
    {
        attackBall.SetActive(false);
        isAttacking = false;
        timeLastAttackEnded = Time.time;
    }
}


