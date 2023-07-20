using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : IAttackController
{
    
    attackData attDat;

    [SerializeField]
    float attackSpeed, minAttackRange, maxAttackRange, timeLastAttackAttempted, timeLastAttackEnded;
    [SerializeField]
    int attackDamage;
    bool attackCo, attackActive;

    public override attackData getAttackData()
    {
        return new attackData(attackSpeed, minAttackRange, maxAttackRange, attackDamage, attackCo, attackActive, timeLastAttackAttempted, timeLastAttackEnded);
    }
    public override void RequestStartAttack()
    {

    }
    public override void RequestStopAttack()
    {

    }
    protected override IEnumerator AttackCo()
    {
        yield return null;
    }
    protected override void StartAttack()
    {

    }
    protected override void StopAttack()
    {

    }
}
