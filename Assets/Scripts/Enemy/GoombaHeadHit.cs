using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaHeadHit : MonoBehaviour {

    [SerializeField]
    private Transform headPoint;   

    private void OnTriggerEnter2D(Collider2D other)
    {       
        if (other.tag == "Player")
        {
            if (other.transform.position.y > headPoint.position.y)
            {
                other.GetComponent<StompScript>().StompBounce();
                Destroy(gameObject);
            }
            else
            {
                if (other.transform.position.x < transform.position.x)
                {
                    other.transform.position -= transform.right;
                }
                else
                {
                    other.transform.position += transform.right;
                }

                other.GetComponent<PlayerHealth>().LoseLife();
            }           
        }
    }
}
