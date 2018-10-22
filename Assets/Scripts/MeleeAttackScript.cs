using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackScript : MonoBehaviour {
        
    private enum weaponTypes {Sword, Whip}

    [SerializeField]
    private weaponTypes Weapons = weaponTypes.Sword;

    private Animator anim;

    [SerializeField]
    private BoxCollider2D WeaponHitArea;

    [SerializeField]
    private GameObject whip;

    [SerializeField]
    private bool isPlayer;

    private void Start()
    {
        anim = GetComponent<Animator>();       
    }

    private void Update()
    {
        if (isPlayer && Input.GetMouseButtonDown(0))
        {
            switch (Weapons)
            {
                case weaponTypes.Sword:
                    StartCoroutine(Attack());
                    break;
                case weaponTypes.Whip:
                    StartCoroutine(Whip());
                    break;
                default:
                    break;
            }                     
        }
    }

    public IEnumerator Attack()
    {
        if (anim != null)
        {
            WeaponHitArea.enabled = true;
            anim.SetTrigger("attack");
            yield return new WaitForSeconds(0.3f);
            WeaponHitArea.enabled = false;
        }        
    }

    private IEnumerator Whip()
    {
        whip.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        WeaponHitArea.enabled = true;        
        yield return new WaitForSeconds(0.2f);
        WeaponHitArea.enabled = false;
        whip.SetActive(false);
    }
}
