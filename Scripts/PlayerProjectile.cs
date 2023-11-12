using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 10.0f;
    public float detectionRadius = 5.0f;
    public int damage = 20;
    public float turnSpeed = 2.0f; // The speed at which the projectile turns towards the enemy
    private Transform target;

    // Update is called once per frame
    void Update()
    {
        // Find the closest enemy within the detection radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        float shortestDistance = Mathf.Infinity;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    target = hitCollider.transform;
                }
            }
        }

        // Rotate towards the target if one was found, otherwise move in the initial direction
        if (target != null)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // Move forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Damage the enemy
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Destroy the projectile
            Destroy(gameObject);
        } 
    }
}