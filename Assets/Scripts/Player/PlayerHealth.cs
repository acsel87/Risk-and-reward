using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private float healSpeed;

    [SerializeField]
    private Animation healthAnimation;

    [SerializeField]
    private Image healthVignette;

    [SerializeField]
    private List<GameObject> lives;

    public int health = 160;

    private bool lostLife = false;

    private Animation animat;
    private Color32 originalHealthColor;
    private int originalHealth;
    private float timeToHeal;

    private void Start()
    {        
        if (healthVignette != null)
        {
            originalHealthColor = healthVignette.color;
            originalHealth = health;
        }

        animat = GetComponent<Animation>();
    }

    private void Update()
    {
        Heal();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        originalHealthColor.a = (byte)Mathf.Round(161 - health);        
        healthVignette.color = originalHealthColor;

        HealthFlicker();

        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }

        Debug.Log(health);
    }

    public void LoseLife()
    {
        if (lives.Count > 0 && !lostLife)
        {
            lives[lives.Count - 1].GetComponent<Animation>().Play();
            lives.RemoveAt(lives.Count - 1);

            StartCoroutine(LostLife());

            if (lives.Count <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void HealthFlicker()
    {
        if (health < originalHealth / 3 && !healthAnimation.isPlaying)
        {
            healthAnimation.Play();            
        }
    }

    private void Heal()
    {
        if (healthVignette != null)
        {
            if (health > 0 && health < originalHealth - 10f && Time.time > timeToHeal)
            {
                timeToHeal = Time.time + 1 / healSpeed;
                health += 10;
                originalHealthColor.a = (byte)Mathf.Round(originalHealth - health);
                healthVignette.color = originalHealthColor;
                Debug.Log(health);
            }

            if (health > originalHealth / 3 && healthAnimation.isPlaying)
            {
                healthAnimation.Stop();
            }
        }       
    }    

    IEnumerator LostLife()
    {
        lostLife = true;
        gameObject.layer = 2;
        animat.Play();
        yield return new WaitForSeconds(3f);
        animat.Stop();
        gameObject.layer = 8;
        lostLife = false;
    }
}
