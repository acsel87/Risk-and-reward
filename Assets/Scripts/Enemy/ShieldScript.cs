using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {

    [SerializeField]
    private GameObject shield; 
    
    private CircleCollider2D col;   
	
    private void Start()
    {
        col = GetComponent<CircleCollider2D>();        
    }

    void Update () {
        CheckIfShieldUp();
	}

    private void CheckIfShieldUp()
    {
        if (StudyScript.grades > 35f)
        {
            shield.SetActive(false);
            col.enabled = true;           
        }
        else
        {            
            col.enabled = false;
            shield.SetActive(true);
        }
    }
}
