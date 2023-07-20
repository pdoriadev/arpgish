using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
*/


[RequireComponent(typeof(Alertable))]
[RequireComponent(typeof(IAttackController))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAIv2 : MonoBehaviour
{
    Alertable alertable;
    List<IAttackController> attacks = new List<IAttackController>();

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

        IAttackController[] cArr = GetComponents<IAttackController>();
        foreach(IAttackController c in cArr)
        {
            attacks.Add(c);
        }
        if (attacks.Count == 0)
        {
            Debug.LogError("Missing IAttackController component(s)");
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
            attackData attDat = attacks[0].getAttackData();
            if (attDat.attActive == false)
            {
                if (toAlerter.magnitude > attDat.minAttRange)
                {
                    Vector3 targetToEnemy = transform.position - target.position;
                    Vector3 newDest = target.position + targetToEnemy.normalized * attacks[0].getAttackData().minAttRange;
                    agent.destination = newDest;
                }

                if (toAlerter.magnitude < attDat.maxAttRange)
                {
                    attacks[0].RequestStartAttack();
                }
            }
            else
            {
                if (toAlerter.magnitude > attDat.maxAttRange)
                {
                    attacks[0].RequestStopAttack();
                }
            }
        }

        lastPosition = transform.position;
    }
}
