using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaHeadHit : MonoBehaviour {

    [SerializeField]
    private Transform headPoint;

    private EnemyPingPongMove enemyPingPongMove;
    private Animator anim;
    private BoxCollider2D boxCol;
    private CircleCollider2D circleCol;

    private void Start()
    {
        enemyPingPongMove = GetComponent<EnemyPingPongMove>();
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        circleCol = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {       
        if (other.tag == "Player")
        {
            if (other.transform.position.y > headPoint.position.y)
            {
                other.GetComponent<StompScript>().StompBounce();
                StartCoroutine(Die());
            }
            else
            {
                //if (other.transform.position.x < transform.position.x)
                //{
                //    other.transform.position -= transform.right;
                //}
                //else
                //{
                //    other.transform.position += transform.right;
                //}

                other.GetComponent<PlayerHealth>().LoseLife();
            }           
        }
    }

    private IEnumerator Die()
    {
        enemyPingPongMove.enabled = false;
        boxCol.enabled = false;
        circleCol.radius = 0.1f;
        circleCol.offset = new Vector2(0f, -0.5f);
        anim.SetTrigger("die");
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
