using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {
    
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform meleeAreaTrigger;
    [SerializeField]
    private Transform longRangeAreaTrigger;

    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float meleeRate;
    [SerializeField]
    private float longRangeRate;

    private MeleeAttackScript meleeScript;
    
    private bool attackMelee;
    private bool attackLongRange;    

    private float meleeTimeToFire = 0f;
    private float longRangeTimeToFire = 0f;

    void Start () {
        meleeScript = GetComponent<MeleeAttackScript>();
	}	
	
	void Update () {
        CheckPlayerRange();        
	}

    private void Attack()
    {
        if (attackMelee && Time.time > meleeTimeToFire)
        {
            meleeTimeToFire = Time.time + 1 / meleeRate;
            StartCoroutine(meleeScript.Attack());
        }
        else if (attackLongRange && Time.time > longRangeTimeToFire)
        {
            longRangeTimeToFire = Time.time + 1 / longRangeRate;
            Projectile();
        }
    }

    private void CheckPlayerRange()
    {
        if (player != null)
        {
            if (player.position.x < meleeAreaTrigger.position.x && player.position.x > longRangeAreaTrigger.position.x)
            {
                attackMelee = false;
                attackLongRange = true;
            }
            else if (player.position.x > meleeAreaTrigger.position.x)
            {
                attackLongRange = false;
                attackMelee = true;
            }
            else
            {
                attackLongRange = false;
            }

            Attack();
        }        
    }   

    private void Projectile()
    {
        Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    }
}
