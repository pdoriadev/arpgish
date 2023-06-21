using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{
    
    NavMeshAgent agent;
    Vector3 lastPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
            Debug.LogError("Failed to retrieve NavMeshAgent from gameobject");
    }

    private void FixedUpdate()
    {
        if (transform.position == lastPosition)
        {
            agent.destination = lastPosition + (3 *
                new Vector3(Mathf.Lerp(-Time.time % 5, Time.time % 5, (Time.time % 5) / 5), 
                    Mathf.Lerp(-Time.time % 3, Time.time % 3, (Time.time % 3) / 3),
                    Mathf.Lerp(-Time.time % 9, Time.time % 9, (Time.time % 9) / 9)));
        }
        

        lastPosition = transform.position;
    }
}
