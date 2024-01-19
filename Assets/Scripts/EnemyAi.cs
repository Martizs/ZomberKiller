using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    Transform target;

    [SerializeField]
    float chaseRange = 5f;

    [SerializeField]
    float turnSpeed = 1f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;

    bool isProvoked = false;

    EnemyHealth enemyHealth;

    [SerializeField]
    AudioClip walkingSound;

    [SerializeField]
    AudioClip attackingSound;

    AudioSource audioSource;

    EndGameHandler endGameHandler;

    public enum ZombieAction
    {
        Idle,
        Walking,
        Attacking,
        Dying,
    }

    ZombieAction zombieAction = ZombieAction.Idle;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        audioSource = GetComponent<AudioSource>();
        target = FindObjectOfType<PlayerHealth>().transform;
        endGameHandler = FindObjectOfType<EndGameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.IsDead() || endGameHandler.GameOver)
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }
        else
        {
            distanceToTarget = Vector3.Distance(target.position, transform.position);

            if (isProvoked)
            {
                EngageTarget();
            }
            else if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;
            }
        }
    }

    void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget < navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void ChaseTarget()
    {
        if (zombieAction != ZombieAction.Walking)
        {
            audioSource.Stop();
            zombieAction = ZombieAction.Walking;
            audioSource.clip = walkingSound;
            audioSource.Play();
        }

        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.transform.position);
    }

    void AttackTarget()
    {
        if (zombieAction != ZombieAction.Attacking)
        {
            audioSource.Stop();
            zombieAction = ZombieAction.Attacking;
            audioSource.clip = attackingSound;
            audioSource.Play();
        }
        FaceTarget();
        GetComponent<Animator>().SetBool("attack", true);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * turnSpeed
        );
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    public void GameOver()
    {
        audioSource.Stop();
    }
}
