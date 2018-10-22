using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladosShootingScript : MonoBehaviour {
       
    [SerializeField]
    private LayerMask whatToHit;
    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private int damage;
    [SerializeField]
    private float range;

    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private float fireRate;

    private float timeToFire = 0f;

    private EnemyPingPongMove pingPongMove;

    private void Start()
    {
        pingPongMove = GetComponent<EnemyPingPongMove>();    
    }

    private void Update()
    {
        CheckPlayerSight();
    }

    private void CheckPlayerSight()
    {
        if (playerHealth != null)
        {
            Vector2 direction = new Vector2(playerHealth.transform.position.x, playerHealth.transform.position.y + 0.5f) - (Vector2)transform.position;
            direction.Normalize();

            if (direction.sqrMagnitude < range * range)
            {
                RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, 100f, whatToHit);

                Debug.DrawRay(firePoint.position, direction, Color.red);

                if (hit && hit.transform.tag == "Player")
                {
                    pingPongMove.enabled = false;
                    Shoot();
                }
                else
                {
                    pingPongMove.enabled = true;
                }
            }
        }
    }

    private void Shoot()
    {        
        if (Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            playerHealth.TakeDamage(damage);
        }
    }
}
