using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 1.0f;
    public float shootingDistance = 10.0f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5.0f;
    public float reloadTime = 1.0f;
    private float timeSinceLastShot = 0.0f;
    // Update is called once per frame

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > shootingDistance)
        {
            // Move towards the player
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);
        }
        else if (timeSinceLastShot >= reloadTime)
        {
            // Shoot at the player
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * projectileSpeed;
            Destroy(projectile, 5.0f);

            // Reset the time since the last shot
            timeSinceLastShot = 0.0f;
        }

        // Increase the time since the last shot
        timeSinceLastShot += Time.deltaTime;
    }
}
