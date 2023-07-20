using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class attackData
{
    public float attPerSec, attTime, minAttRange, maxAttRange, timeLastAttStarted, timeLastAttackEnded;
    public int damage;
    public bool attackCoroutineActive, attActive;

    public attackData()
    {
        attPerSec = 1f;
        attTime = 0.5f;
        minAttRange = 1f;
        maxAttRange = 2f;
        timeLastAttStarted = 0f;
        timeLastAttackEnded = 0f;

        damage = 1;

        attackCoroutineActive = false;
        attActive = false;
    }

    public attackData(float _speed, float _minAttackRange, float _maxAttackRange,
            int _damage, bool _attackCo, bool _attackActive, float _timeLastAttackAttempted, float _timeLastAttackEnded)
    {
        attPerSec = _speed;
        minAttRange = _minAttackRange;
        maxAttRange = _maxAttackRange;
        damage = _damage;
        attackCoroutineActive = _attackCo;
        attActive = _attackActive;
        timeLastAttStarted = _timeLastAttackAttempted;
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
