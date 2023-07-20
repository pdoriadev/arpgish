using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Alertable))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyAIv3 : MonoBehaviour
{
    Alertable alertable;
    NavMeshAgent agent;

    Vector3 lastPosition;
    Transform target;

    EnemyAIState enemyState = EnemyAIState.IDLE;

    [SerializeField]
    attackData attDat;
    [SerializeField]
    GameObject attBall;

    private void Awake()
    {
        alertable = GetComponent<Alertable>();
        if (alertable == null)
        {
            Debug.LogError("Missing Alertable component");
        }

        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("Missing NavMeshAgent component");
        }
    }

    void Update()
    {
        if (enemyState == EnemyAIState.IDLE)
        {
            if (alertable.alerterList.Count > 0)
            {
                enemyState = EnemyAIState.AGGRO;
                target = alertable.alerterList[0];
            }
        }
        else if (enemyState == EnemyAIState.AGGRO)
        {
            if (alertable.alerterList.Count == 0)
            {
                enemyState = EnemyAIState.IDLE;
                target = null;
            }
        }

        Move();
    }

    void Move()
    {
        if (enemyState == EnemyAIState.IDLE)
        {
            if (lastPosition == agent.destination)
            {
                Vector3 newDest = transform.position + new Vector3(Time.time % 4, Time.time % 4, Time.time % 4) * (Time.time % 3);
                agent.destination = newDest;
            }
        }
        else if (enemyState == EnemyAIState.AGGRO && target != null)
        {
            Vector3 toAlerter = target.position - transform.position;
            if (attDat.attActive == false)
            {
                if (toAlerter.magnitude > attDat.minAttRange)
                {
                    Vector3 targetToEnemy = transform.position - target.position;
                    Vector3 newDest = target.position + targetToEnemy.normalized * attDat.minAttRange;
                    agent.destination = newDest;
                }

                if (toAlerter.magnitude < attDat.maxAttRange)
                {
                    RequestStartAttack();
                }
            }
            else
            {
                if (toAlerter.magnitude > attDat.maxAttRange)
                {
                    RequestStopAttack();
                }
            }
        }

        lastPosition = transform.position;
    }

    void RequestStartAttack()
    {
        if (attDat.attackCoroutineActive == false)
        {
            attDat.attackCoroutineActive = true;
            StartCoroutine(AttackCo());
        }
    }

    void RequestStopAttack()
    {
        if (attDat.attackCoroutineActive)
        {
            attDat.attackCoroutineActive = false;
            StopCoroutine(AttackCo());
            StopAttack();
        }
    }

    protected IEnumerator AttackCo()
    {
        StartAttack();
        yield return new WaitForSeconds(1 / attDat.attTime);
        StopAttack();
        yield return new WaitForSeconds(1 / attDat.attPerSec);

    }

    void StartAttack()
    {
        attDat.attActive = true;
        attBall.SetActive(true);
        attDat.timeLastAttStarted = Time.time;
    }

    void StopAttack()
    {
        attBall.SetActive(false);
        attDat.attActive = false;
        attDat.timeLastAttackEnded = Time.time;
    }
}
