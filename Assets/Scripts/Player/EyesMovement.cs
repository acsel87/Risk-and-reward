using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EyesMovement : MonoBehaviour {

    [SerializeField]
    private GameObject HUD;   
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CinemachineVirtualCamera eyes_vCam;
    [SerializeField]
    private CinemachineVirtualCamera player_vCam;

    [SerializeField]
    private Transform startAreaMargin;
    [SerializeField]
    private Transform startAreaTrigger;
    [SerializeField]
    private Transform growPoint;      

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float oscilationSpeed;
    [SerializeField]
    private float oscilationLength;
    [SerializeField]
    private bool isSecondLevel;

    private SpriteRenderer spriteRenderer;
    private Vector3 tempScale;

    private float oscilationY;
    private float originalGrowPointY;
    private float originalY;

    void Start()
    {
        oscilationY = transform.position.y;
        originalGrowPointY = growPoint.position.y;
        originalY = transform.position.y;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Oscilate();
    }

    private void Move()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 )
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;           

            if (isSecondLevel && oscilationY < originalGrowPointY)
            {
                oscilationY += 0.5f * Time.deltaTime;
                transform.localScale -= Vector3.one * 0.1f * Time.deltaTime;
            }

            if (spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
        }   
        else if (transform.position.x > startAreaMargin.position.x && Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.position += -transform.right * moveSpeed * Time.deltaTime;
           

            if (isSecondLevel && oscilationY > originalY)
            {
                oscilationY -= 0.5f * Time.deltaTime;
                transform.localScale += Vector3.one * 0.1f * Time.deltaTime;
            }

            if (!spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    private void Oscilate()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
           transform.position = new Vector2(transform.position.x, oscilationY + Mathf.Sin(Time.time * oscilationSpeed) * oscilationLength);
        }       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Respawn")
        {
            spriteRenderer.enabled = false;
            player.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Respawn")
        {
            other.GetComponent<BoxCollider2D>().isTrigger = false;
            eyes_vCam.Priority = 1;
            HUD.SetActive(true);            
            gameObject.SetActive(false);
        }
    }
}
