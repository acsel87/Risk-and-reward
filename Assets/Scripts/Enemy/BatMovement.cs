﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float oscilationSpeed;   

    private float originalY;

    private SpriteRenderer sr;
    private float timeCounter;
    
	void Start () {
        sr = GetComponent<SpriteRenderer>();

        originalY = transform.position.y;

        Destroy(gameObject, 30f);
	}
	
	void Update () {
        Fly();
        Oscilate();
	}

    private void Fly()
    {
        if (sr.flipX)
        {
            transform.position += -transform.right * moveSpeed * Time.deltaTime;
        }
        if (!sr.flipX)
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
    }

    private void Oscilate()
    {
        timeCounter += Time.deltaTime;
        transform.position = new Vector2(transform.position.x, originalY + Mathf.Sin(timeCounter * oscilationSpeed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { 
            other.GetComponent<PlayerHealth>().LoseLife();            
        }
    }
}
