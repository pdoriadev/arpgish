using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeAttacker : MonoBehaviour, IAttacker
{

    [SerializeField]
    int attackDamage = 1;
    [SerializeField]
    float attackSpeed = 1f, closestMeleeRange = 1.5f, maxMeleeRange = 2f, attackTime = 0.3f;

    bool isAttacking = false;
    float timeSinceAttackAttempted = 0f, timeSinceAttackEnded = 0f;

    [SerializeField]
    GameObject attackBall;


    public attackData getAttackData()
    {
        return new attackData(attackSpeed, closestMeleeRange, maxMeleeRange, attackDamage, isAttacking, timeSinceAttackAttempted, timeSinceAttackEnded);
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
        timeSinceAttackAttempted = 0f;
        attackBall.SetActive(true);
    }

    void StopAttack()
    {
        attackBall.SetActive(false);
        isAttacking = false;
    }
    
}
