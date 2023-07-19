using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct attackData
{
    public float attackSpeed, minAttackRange, maxAttackRange, timeLastAttackAttempted, timeLastAttackEnded;
    public int attackDamage;
    public bool attackCo, attackActive;

    public attackData(float _speed, float _minAttackRange, float _maxAttackRange,
            int _damage, bool _attackCo, bool _attackActive, float _timeLastAttackAttempted, float _timeLastAttackEnded)
    {
        attackSpeed = _speed;
        minAttackRange = _minAttackRange;
        maxAttackRange = _maxAttackRange;
        attackDamage = _damage;
        attackCo = _attackCo;
        attackActive = _attackActive;
        timeLastAttackAttempted = _timeLastAttackAttempted;
        timeLastAttackEnded = _timeLastAttackEnded;
    }
}

public abstract class IAttackController : MonoBehaviour
{
    public abstract attackData getAttackData();
    public abstract void RequestStartAttack();
    public abstract void RequestStopAttack();
    protected abstract IEnumerator AttackCo();
    protected abstract void StartAttack();
    protected abstract void StopAttack();

}
