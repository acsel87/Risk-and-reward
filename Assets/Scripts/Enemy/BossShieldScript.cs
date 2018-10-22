using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldScript : MonoBehaviour {

    [SerializeField]
    private GameObject shield;

    [SerializeField]
    private StudyScript playerStudy;

    [SerializeField]
    private float bossStunTime;

    [SerializeField]
    private Sprite stunSprite;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private MeleeAttackScript meleeAttackScript;
    private BossScript bossScript;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        meleeAttackScript = GetComponent<MeleeAttackScript>();
        bossScript = GetComponent<BossScript>();        
    }

    void Update()
    {
        CheckIfShieldUp();
    }

    private void CheckIfShieldUp()
    {
        if (StudyScript.grades >= 99f)
        {
            StartCoroutine(ShatterShield());
        }        
    }

    IEnumerator ShatterShield()
    {
        Debug.Log("boss shield shattered");

        ToggleBossMovement();
        spriteRenderer.sprite = stunSprite;
        // player animation
        // shatter shield animation

        shield.SetActive(false);
        StudyScript.grades = 0f;
        yield return new WaitForSeconds(0.1f);
        playerStudy.enabled = false;

        yield return new WaitForSeconds(bossStunTime);

        ToggleBossMovement();

        shield.SetActive(true);
        
        playerStudy.enabled = true;
    }

    private void OnDisable()
    {
        playerStudy.enabled = true;
    }

    private void ToggleBossMovement()
    {        
        anim.enabled = !anim.enabled;
        meleeAttackScript.enabled = !meleeAttackScript.enabled;
        bossScript.enabled = !bossScript.enabled;        
    }
}
