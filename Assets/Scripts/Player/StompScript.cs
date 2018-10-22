using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompScript : MonoBehaviour {

    [SerializeField]
    private float gravityMultiplier;

    private PlayerMovement player;
    private Rigidbody2D rb;	

	void Start () {       
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
	}
		
	void Update () {
        CheckIfStomp();
	}

    private void CheckIfStomp()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !player.grounded)
        {
            Stomp();
        }
    }

    private void Stomp()
    {
        player.fallMultiplier = gravityMultiplier;
    }

    public void StompBounce()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(transform.up * 7f, ForceMode2D.Impulse);
    }
}
