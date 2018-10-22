using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health = 100;

    private Animator anim;

    [SerializeField]
    private bool isBoss;

    private bool canTakeDamage = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            health -= damage;

            if (isBoss && anim != null)
            {
                anim.SetTrigger("hurt");
            }

            if (health <= 0)
            {
                health = 0;
                Die();
            }

            StartCoroutine(Damaged());
        }        
    }

    private IEnumerator Damaged()
    {
        canTakeDamage = false;        
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }

    private void Die()
    {
        // die effect
        Destroy(gameObject);
    }    
}
