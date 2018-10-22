using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    private enum SecondaryTypes { Fireball, Grenade }

    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private SecondaryTypes projectile;
    [SerializeField]
    private bool isTargetPlayer;
    [SerializeField]
    private bool isFirstLevel;
    [SerializeField]
    private float explosionRadius;   

    public int damage = 20;

    private Vector2 targetPoint;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");        

        if (isTargetPlayer)
        {
            Shoot(player.transform.position);
        }
        else
        {
            if (isFirstLevel)
            {
                Shoot(transform.position + transform.right * player.transform.localScale.x);
            }
            else
            {
                Shoot(AimScript.mousePoint);
            }
        }        
    }    

    private void Shoot(Vector2 target)
    {
        switch (projectile)
        {
            case SecondaryTypes.Fireball:
                Fireball(target);
                break;
            case SecondaryTypes.Grenade:
                break;
            default:
                break;
        }
    }

    private void Fireball(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float AngleRad = Mathf.Atan2(direction.y, direction.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        rb.velocity = Vector2.zero;
        rb.velocity = direction * speed * Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTargetPlayer)
        {
            PlayerHealth PlayerHealth = other.GetComponent<PlayerHealth>();

            if (PlayerHealth != null && isFirstLevel)
            {
                PlayerHealth.LoseLife();
            }
            else if (PlayerHealth != null && !isFirstLevel)
            {
                PlayerHealth.TakeDamage(damage);
            }
        }

        else
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<EnemyHealth>().TakeDamage(damage); ;                
            }
            
            RocketShoot(transform.position);
        }

        OnHit();
    }

    private void RocketShoot(Vector2 hitPoint)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPoint, explosionRadius);

        foreach (Collider2D col in colliders)
        {            
            if (col.tag == "Enemy")
            {
                col.GetComponent<EnemyHealth>().TakeDamage(damage);
                break;
            }
            else if (col.tag == "Player")
            {
                col.GetComponent<PlayerHealth>().TakeDamage(damage);
                break;
            }
        }        
    }    

    private void OnHit()
    {
        Destroy(gameObject);
    }
}
