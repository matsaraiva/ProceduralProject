using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileParent;
    public float fireRate = 0.5f;
    private float nextFireTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            // Fire a projectile
            GameObject projectile = Instantiate(projectilePrefab, projectileParent.position, projectileParent.rotation);


            // Set the next fire time
            nextFireTime = Time.time + fireRate;

            // Destroy the projectile after 5 seconds
            Destroy(projectile, 5.0f);
        }
    }
}
