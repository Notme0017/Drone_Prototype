using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyAi : MonoBehaviour
{
    [Header("Patrol")]
    public GameObject[] patrolPoints;
    private int currentPatrolIndex;

    [Header("References")]
    public Animator anim;
    public NavMeshAgent navMeshAgent;
    public EnemyAttack attack;

    [SerializeField]
    private GameObject drone;

    private GameObject target;

    [Header("Detection")]
    public float detectionRadius = 4.0f;

    private bool shootTriggered;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Let NavMeshAgent handle rotation
        navMeshAgent.updateRotation = true;
    }

    private void Start()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            patrolPoints = GameObject
                .FindGameObjectsWithTag("PatrolPoint")
                .OrderBy(go => go.name)
                .ToArray();
        }

        currentPatrolIndex = 0;

        if (drone == null)
        {
            drone = GameObject.FindGameObjectWithTag("Drone");
        }

        target = patrolPoints.Length > 0 ? patrolPoints[0] : null;
    }

    private void Update()
    {
        bool droneDetected = DetectDrone();
        anim.SetBool("playerDetected", droneDetected);
        if (droneDetected)
        {
            AttackDrone();
        }
        else
        {
            Patrol();
        }
    }

    // -------------------- PATROL --------------------

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        shootTriggered = false;
        attack.canAttack = false;

        anim.SetBool("isPatrolling", true);

        navMeshAgent.isStopped = false;

        target = patrolPoints[currentPatrolIndex];
        navMeshAgent.SetDestination(target.transform.position);

        if (Vector3.Distance(transform.position, target.transform.position) < 1.0f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    // -------------------- ATTACK --------------------

    private void AttackDrone()
    {
        if (drone == null) return;

        target = drone;

        navMeshAgent.isStopped = true;

        anim.SetBool("isPatrolling", false);

        // Fire trigger ONCE
        if (!shootTriggered)
        {
            anim.SetTrigger("isShooting");
            shootTriggered = true;
        }
        attack.canAttack = true;

        FaceTarget(drone.transform.position);
    }

    // -------------------- DETECTION --------------------

    private bool DetectDrone()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Drone"))
            {
                return true;
            }
        }
        return false;
    }

    // -------------------- ROTATION --------------------

    private void FaceTarget(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude < 0.01f) return;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            lookRotation,
            Time.deltaTime * 5f
        );
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
#endif
}

