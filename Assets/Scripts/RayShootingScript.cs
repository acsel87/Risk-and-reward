using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShootingScript : MonoBehaviour {
    
    public float mainFireRate = 0f;
    public int mainDamage = 10;
      
    public LayerMask whatToHit;
    public LineRenderer line;

    [SerializeField]
    private Transform firePoint;

    private Vector2 direction;

    private float mainTimeToFire = 0f;    

    private void Update()
    {
        CheckIfShoot();
    }

    IEnumerator Shoot()
    {
        direction = AimScript.mousePoint - (Vector2)firePoint.position;
        direction.Normalize();
        
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, 100f, whatToHit);

        line.SetPosition(0, firePoint.position);

        if (hit)
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(mainDamage);
            }
            
            line.SetPosition(1, hit.point);

            // impact effect            
        }
        else
        {            
            line.SetPosition(1, AimScript.mousePoint);
        }

        line.enabled = true;

        yield return 0.5f;

        line.enabled = false;
    }

    public void CheckIfShoot()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (mainFireRate == 0)
            {
                Shoot();
            }   
        }
        else if (Input.GetMouseButton(0) && Time.time > mainTimeToFire)
        {
            mainTimeToFire = Time.time + 1 / mainFireRate;
            StartCoroutine(Shoot());
        }
    }
}