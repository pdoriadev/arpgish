using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyAIState
{
    IDLE,
    AGGRO
}

public class EnemyAI : MonoBehaviour, IAlertable
{

    public float closestMeleeRange = 1.5f;
    public float maxMeleeRange = 2f;

    [SerializeField]
    GameObject attackBall;

    NavMeshAgent agent;
    Vector3 lastPosition;
    EnemyAIState enemyState = EnemyAIState.IDLE;
    List<Transform> alerterList = new List<Transform>();
    bool isAttacking = false;
    float attacksPerSecond = 0.3f;
    float timeSinceLastAttack = 0f;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
            Debug.LogError("Failed to retrieve NavMeshAgent from gameobject");
    }

    private void FixedUpdate()
    {
        timeSinceLastAttack += Time.fixedDeltaTime;
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
            Vector3 enemyToPlayer = (transform.position - alerterList[0].position);
            if (enemyToPlayer.magnitude < maxMeleeRange && !isAttacking && timeSinceLastAttack > 1/attacksPerSecond)
            {
                StartCoroutine( AttackCo());
            }
            else
            {
                Vector3 newDest = alerterList[0].position + enemyToPlayer.normalized * closestMeleeRange;
                agent.destination = newDest;
            }
        }
    }
    
    IEnumerator AttackCo()
    {
        isAttacking = true;
        while (isAttacking)
        {
            attackBall.SetActive(true);
            yield return null;
            attackBall.SetActive(false);
            isAttacking = false;
            timeSinceLastAttack = 0;
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
