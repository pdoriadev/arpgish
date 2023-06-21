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
    
    NavMeshAgent agent;
    Vector3 lastPosition;
    EnemyAIState enemyState = EnemyAIState.IDLE;
    List<Transform> alertersList = new List<Transform>();

    float timeLastReachedDestinationWhenIdle;

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
            if (alertersList.Count > 0)
            {
                enemyState = EnemyAIState.AGGRO;
            }
        }
        else if (alertersList.Count == 0)
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
            agent.destination = alertersList[0].position;
        }

    }

    public void RequestAlert(Transform alerterTransform)
    {
        if (alerterTransform != null)
        {
            alertersList.Add(alerterTransform);
            return;
        }

        Debug.LogError("Transform is null");

        
    }
    public void RequestSilence(Transform alerterTransform)
    {
        if (alerterTransform != null)
        {
            alertersList.Remove(alerterTransform);
            return;
        }

        Debug.LogError("Transform is null");
    }
}
