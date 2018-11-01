using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPingPongMove : MonoBehaviour {
          
    [SerializeField]
    private Transform leftLimit;
    [SerializeField]
    private Transform rightLimit;
    [SerializeField]
    private float speed;

    private float originalLeftLimit;
    private float originalRightLimit;

    private Vector3 normalScale = new Vector3(1, 1, 1);
    private Vector3 flippedScale = new Vector3(-1, 1, 1);
    private float distance;

    private float timeCounter;

    void Start () {
        originalLeftLimit = leftLimit.position.x;
        originalRightLimit = rightLimit.position.x;
        distance = (leftLimit.position - rightLimit.position).magnitude;
    }
	
	void Update () {
        PingPongMove();        
	}

    private void PingPongMove()
    {
        timeCounter += Time.deltaTime;

        float time = Mathf.PingPong(timeCounter * speed * 1 / distance, 1);
        Vector2 newPos = new Vector2(Mathf.Lerp(originalRightLimit, originalLeftLimit, time), transform.position.y);        

        transform.position = newPos;

        if (time > .95f && transform.localScale.x > 0)
        {
            transform.localScale = flippedScale;
        }
        else if (time < 0.05f && transform.localScale.x < 0)
        {
            transform.localScale = normalScale;
        }
    }
}
