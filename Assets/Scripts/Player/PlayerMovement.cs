using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField]
    private GameObject eyes;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float jumpPower = 5f;
    [SerializeField]
    public float fallMultiplier = 2.5f;
    [SerializeField]
    private float lowJumpMultiplier = 2f;
    [SerializeField]
    private float gravityNormalScale = 5f;
    [SerializeField]
    private bool scaleOnTurn = false;

    private float xAxis;
    private bool jump = false;
    private float initialFallMultiplier;

    [HideInInspector]
    public bool grounded = true;
    
    private Rigidbody2D rb;
    private EdgeCollider2D groundCol;
    private Animator anim;

    private bool turned = false;

	void Start () {        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        initialFallMultiplier = fallMultiplier;
	}

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        ScaleXOnTurn();

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

        StartAnimation();
    }

    void FixedUpdate () {
        Move();
        Jump();        
    }

    private void ScaleXOnTurn()
    {
        if (scaleOnTurn)
        {
            if (xAxis < 0 && !turned)
            {
                turned = true;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (xAxis > 0 && turned)
            {
                turned = false;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(xAxis * speed, rb.velocity.y);
    }

    private void Jump()
    {      
        if (jump)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce( Vector2.up * jumpPower, ForceMode2D.Impulse);            
            jump = false;
            grounded = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = gravityNormalScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            grounded = true;            
        }

        fallMultiplier = initialFallMultiplier;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "StartChange" && transform.position.x < other.transform.position.x)
        {
            Vector2 newPos = transform.position;
            newPos.x = other.transform.position.x - 0.5f;
            eyes.transform.position = newPos;

            transform.position = newPos + (Vector2)transform.right * 2f;

            eyes.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (other.tag == "EndChange" && transform.position.x > other.transform.position.x)
        {
            Vector2 newPos = transform.position;
            newPos.x = other.transform.position.x + 0.5f;
            eyes.transform.position = newPos;

            transform.position = newPos - (Vector2)transform.right * 2f;

            eyes.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void StartAnimation()
    {
        if (anim != null)
        {
            anim.SetInteger("xAxis", (int)xAxis);
        }
    }
}
