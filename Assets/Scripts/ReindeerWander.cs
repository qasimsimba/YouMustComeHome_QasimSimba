using UnityEngine;
using UnityEngine.AI;

public class ReindeerWander : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 6f;

    private NavMeshAgent agent;
    private Animator anim;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Forced Unity reference
        timer = UnityEngine.Random.Range(0, wanderTimer);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            // We call the method below
            UnityEngine.Vector3 newPos = RandomNavMeshLocation(wanderRadius);
            agent.SetDestination(newPos);
            timer = 0;
        }

        if (anim != null && agent != null)
        {
            float currentSpeed = agent.velocity.magnitude;
            anim.SetFloat("Vert", currentSpeed);
        }
    }

    public UnityEngine.Vector3 RandomNavMeshLocation(float radius)
    {
        // Forced Unity reference
        UnityEngine.Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            return hit.position;
        }
        return transform.position;
    }
}