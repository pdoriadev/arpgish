using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
*/


[RequireComponent(typeof(Alertable))]
[RequireComponent(typeof(MeleeAttacker))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAIv2 : MonoBehaviour
{
    Alertable alertable;
    MeleeAttacker melee;

    NavMeshAgent agent;
    Vector3 lastPosition;
    Transform target;
    EnemyAIState enemyState = EnemyAIState.IDLE;

    private void Awake()
    {
        alertable = GetComponent<Alertable>();
        if (alertable == null)
        {
            Debug.LogError("Missing Alertable component");
        }
        melee = GetComponent<MeleeAttacker>();
        if (melee == null)
        {
            Debug.LogError("Missing MeleeAttacker component");
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
            attackData attData = melee.getAttackData();
            if (attData.isAttacking == false)
            {
                if (toAlerter.magnitude > attData.minAttackRange)
                {
                    Vector3 targetToEnemy = transform.position - target.position;
                    Vector3 newDest = target.position + targetToEnemy.normalized * melee.getAttackData().minAttackRange;
                    agent.destination = newDest;
                }

                if (toAlerter.magnitude < attData.maxAttackRange)
                {
                    melee.RequestAttack();
                }
            }
            else
            {
                if (toAlerter.magnitude > attData.maxAttackRange)
                {
                    melee.RequestStopAttack();
                }
            }
        }

        lastPosition = transform.position;
    }
}
