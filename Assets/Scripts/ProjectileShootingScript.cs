using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShootingScript : MonoBehaviour {

    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform firePoint;

    public float secondaryFireRate = 0f;

    private float secondaryTimeToFire = 0f;

    private void Update()
    {
        CheckIfShoot();
    }

    private void Projectile()
    {
        Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    }

    private void CheckIfShoot()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (secondaryFireRate == 0)
            {
                Projectile();
            }
        }
        else if (Input.GetMouseButton(1) && Time.time > secondaryTimeToFire)
        {
            secondaryTimeToFire = Time.time + 1 / secondaryFireRate;
            Projectile();
        }
    }
}
