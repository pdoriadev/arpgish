using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyAIState
{
    IDLE,
    AGGRO
}


public class EnemyAI : MonoBehaviour, AIAlertable
{

    public float meleeRange = 1.5f;

    NavMeshAgent agent;
    Vector3 lastPosition;
    EnemyAIState enemyState = EnemyAIState.IDLE;
    List<Transform> alerterList = new List<Transform>();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
            Debug.LogError("Failed to retrieve NavMeshAgent from gameobject");
    }

    private void FixedUpdate()
    {
        if (enemyState == EnemyAIState.IDLE)
        {
            if (alerterList.Count > 0)
            {
                enemyState = EnemyAIState.AGGRO;
            }
        }
        else if (alerterList.Count == 0)
        {
            enemyState = EnemyAIState.IDLE;
        }


        Move();




    }

    void Move()
    {
        if (enemyState == EnemyAIState.IDLE)
        {
            if (transform.position == lastPosition)
            {
                agent.destination = lastPosition + (3 *
                    new Vector3(Mathf.Lerp(-Time.time % 5, Time.time % 5, (Time.time % 5) / 5),
                        Mathf.Lerp(-Time.time % 3, Time.time % 3, (Time.time % 3) / 3),
                        Mathf.Lerp(-Time.time % 9, Time.time % 9, (Time.time % 9) / 9)));
            }
            lastPosition = transform.position;
            return;
        }

        if (enemyState == EnemyAIState.AGGRO)
        {
            Vector3 newDest = alerterList[0].position + (transform.position - alerterList[0].position).normalized * meleeRange;
            agent.destination = newDest;
        }

    }

    public void RequestAlert(Transform alerterTransform)
    {
        if (alerterTransform != null)
        {
            alerterList.Add(alerterTransform);
            return;
        }

        Debug.LogError("Transform is null");

        
    }
    public void RequestSilence(Transform alerterTransform)
    {
        if (alerterTransform != null)
        {
            alerterList.Remove(alerterTransform);
            return;
        }

        Debug.LogError("Transform is null");
    }
}
