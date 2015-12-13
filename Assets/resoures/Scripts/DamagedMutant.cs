﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamagedMutant : MonoBehaviour {
    float currentHealth = 0;
    float maxHealth = 2000;
    public Text ScoreText;
    public Text Statustext;
    float calculatedHealth;
    public GameObject HealthBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet" && PlayerShooting.revolverEnable == true)
        {
            currentHealth -= 50;
            calculatedHealth = currentHealth / maxHealth;
            setHealthBar(calculatedHealth);
        }
        if (col.gameObject.tag == "Bullet" && PlayerShooting.rifleEnable)
        {
            currentHealth -= 20;
            calculatedHealth = currentHealth / maxHealth;
            setHealthBar(calculatedHealth);
        }
        if (col.gameObject.tag == "Bullet" && PlayerShooting.ShottyEnable)
        {
            currentHealth -= 100;
            calculatedHealth = currentHealth / maxHealth;
            setHealthBar(calculatedHealth);
        }
    }
    void Update()
    {
        if (currentHealth <= 0)
            Die();
    }
    void Die()
    {
        Destroy(gameObject);
        ScoreHandler.score += 1000;   
    }
    public void setHealthBar(float myHealth)
    {
        HealthBar.transform.localScale = new Vector3(myHealth, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
    }

}
