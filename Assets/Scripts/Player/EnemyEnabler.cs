using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnabler : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform enemy = other.transform.GetChild(0);

        if (enemy != null)
        {
            enemy.SetParent(other.transform.parent);
            enemy.gameObject.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
