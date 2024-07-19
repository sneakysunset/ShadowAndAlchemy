using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public string targetTag = "House";
    private float stoppingDistance = 0.1f;

    private Transform target;

    void Update()
    {
        FindClosestTarget();
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    private void FindClosestTarget()
    {
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag(targetTag);
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (GameObject potentialTarget in potentialTargets)
        {
            float distanceToTarget = Vector2.Distance(transform.position, potentialTarget.transform.position);
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                closestTarget = potentialTarget.transform;
            }
        }

        target = closestTarget;
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > stoppingDistance)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Destroy(gameObject);
        }
    }
}
