using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class EyesMovement : MonoBehaviour {

    [SerializeField]
    private GameObject HUD;   
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CinemachineVirtualCamera eyes_vCam;
    [SerializeField]
    private CinemachineVirtualCamera eyes_vCam_NoSoftZone;

    [SerializeField]
    private Transform startAreaMargin;
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

    private void Awake()
    {
        oscilationY = transform.position.y;
        originalGrowPointY = growPoint.position.y;
        originalY = transform.position.y;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {        
        eyes_vCam.Priority = 3;        
        HUD.SetActive(false);
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {      
        if (HUD != null)
        {
            HUD.SetActive(true);
        }       
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
        if (other.tag == "StartChange" || other.tag == "EndChange")
        {
            spriteRenderer.enabled = false;
        }

        if (other.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (other.tag == "Respawn")
        {
            eyes_vCam_NoSoftZone.Priority = Mathf.Abs(eyes_vCam_NoSoftZone.Priority - 4);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "StartChange")
        {            
            Vector2 newPos = transform.position;
            newPos.x = other.transform.position.x - 0.5f;
            transform.position = newPos;
                
            player.transform.position = new Vector2(newPos.x + 2f, player.transform.position.y);

            player.SetActive(true);
            eyes_vCam.Priority = 1;

            gameObject.SetActive(false);
        }
        else if (other.tag == "EndChange")
        {
            Vector2 newPos = transform.position;
            newPos.x = other.transform.position.x + 0.5f;
            transform.position = newPos;

            player.transform.position = new Vector2(newPos.x - 2f, player.transform.position.y);

            player.SetActive(true);
            eyes_vCam.Priority = 1;

            gameObject.SetActive(false);
        }

    }
}
