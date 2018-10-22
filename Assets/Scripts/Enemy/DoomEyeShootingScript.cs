using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomEyeShootingScript : MonoBehaviour {

    [SerializeField]
    private LayerMask whatToHit;
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float fireRate;
    
    private float timeToFire = 0f;

    [SerializeField]
    private float range;

    private void Update()
    {
        CheckPlayerSight();
    }

    private void CheckPlayerSight()
    {
        if (player != null)
        {
            Vector2 direction = new Vector2(player.position.x, player.position.y + 0.5f) - (Vector2)transform.position;            

            if (direction.sqrMagnitude < range * range)
            {
                RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, 100f, whatToHit);

                Debug.DrawRay(firePoint.position, direction, Color.red);

                if (hit && hit.transform == player)
                {
                    Shoot();
                }
            }
        }   
    }

    private void Shoot()
    {
        if (Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        }
    }
   
}
