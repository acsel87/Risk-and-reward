using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeScript : MonoBehaviour {

    [SerializeField]
    private int damage = 30;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
