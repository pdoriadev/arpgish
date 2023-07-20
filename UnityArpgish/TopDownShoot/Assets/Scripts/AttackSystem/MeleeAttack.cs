using System.Collections;
using UnityEngine;

public class MeleeAttack : IAttackController
{
    [SerializeField]
    int attackDamage = 1;
    [SerializeField]
    float attackSpeed = 1f, closestMeleeRange = 1.5f, maxMeleeRange = 2f, attackTime = 0.3f;

    bool attackCo = false, attackActive = false;
    float timeLastAttackAttempted = 0f, timeLastAttackEnded = 0f;

    [SerializeField]
    GameObject attackBall;
    
    public override attackData getAttackData()
    {
        return new attackData(attackSpeed, closestMeleeRange, maxMeleeRange, attackDamage, attackCo, attackActive, timeLastAttackAttempted, timeLastAttackEnded);
    }

    public override void RequestStartAttack()
    {
        if (attackCo == false)
        {
            attackCo = true;
            StartCoroutine(AttackCo());
        }
    }

    public override void RequestStopAttack()
    {
        if (attackCo)
        {
            attackCo = false;
            StopCoroutine(AttackCo());
            StopAttack();
        }
    }

    protected override IEnumerator AttackCo()
    {
        StartAttack();
        yield return new WaitForSeconds(1 / attackTime);
        StopAttack();
        yield return new WaitForSeconds(1 / attackSpeed);

    }

    protected override void StartAttack()
    {
        attackActive = true;
        attackBall.SetActive(true);
        timeLastAttackAttempted = Time.time;
    }

    protected override void StopAttack()
    {
        attackBall.SetActive(false);
        attackActive = false;
        timeLastAttackEnded = Time.time;
    }
}


