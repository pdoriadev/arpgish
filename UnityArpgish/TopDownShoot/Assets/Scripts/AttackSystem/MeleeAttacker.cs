using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct attackData
{
    public float attackSpeed, closestMeleeRange, maxMeleeRange;
    public int attackDamage;
    public bool isAttacking;

    public attackData(float speed, float _closestMeleeRange, float _maxMeleeRange, int damage, bool _isAttacking)
    {
        attackSpeed = speed;
        closestMeleeRange = _closestMeleeRange;
        maxMeleeRange = _maxMeleeRange;
        attackDamage = damage;
        isAttacking = _isAttacking;
    }
}


public class MeleeAttacker : MonoBehaviour, IAttacker
{

    [SerializeField]
    int attackDamage = 1;
    [SerializeField]
    float attackSpeed = 1f, closestMeleeRange = 1.5f, maxMeleeRange = 2f, attackTime = 0.3f;

    bool isAttacking = false;
    float timeSinceLastAttack = 0f;

    [SerializeField]
    GameObject attackBall;


    public attackData getAttackData()
    {
        return new attackData(attackSpeed, closestMeleeRange, maxMeleeRange, attackDamage, isAttacking);
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
        timeSinceLastAttack = 0f;
        attackBall.SetActive(true);
    }

    void StopAttack()
    {
        attackBall.SetActive(false);
        isAttacking = false;
    }
    
}
