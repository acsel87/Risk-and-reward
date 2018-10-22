using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeScript : MonoBehaviour {

    [SerializeField]
    private int damage = 30;
    [SerializeField]
    private bool isFirstLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null && isFirstLevel)
        {
            player.LoseLife();
        }
        else if (player != null && !isFirstLevel)
        {
            player.TakeDamage(damage);
        }
    }
}
