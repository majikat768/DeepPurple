//attaches health to things

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHolder : MonoBehaviour, IDamageable
{

    [SerializeField] public int health = 100;
    //set to whatever health starts with
    private int maxHealth;

    //Health text component
    private Text healthText = null;

    //changes color of health
    private int goodHealth;
    private int goodAvgHealth;
    private int avgHealth;
    private int badAvgHealth;
    private int badHealth;

    void Awake () {
        maxHealth = health;
        healthText = GetComponentInChildren<Text>();

        goodHealth = (int)((float)health * 4.0/5.0);
        Debug.Log(this.gameObject.name + " goodHealth:" + goodHealth);
        goodAvgHealth = (int)((float)health * 3.0 / 5.0);
        Debug.Log(this.gameObject.name + " goodAvgHealth:" + goodAvgHealth);
        avgHealth = (int)((float)health * 2.0/ 5.0);
        Debug.Log(this.gameObject.name + " avgHealth:" + avgHealth);
        badAvgHealth = (int)((float)health * 1.0 / 5.0);
        Debug.Log(this.gameObject.name + " badAvgHealth:" + badAvgHealth);
        badHealth = (int)((float)health * 0.5 / 5.0);
        Debug.Log(this.gameObject.name + " badHealth:" + badHealth);
    }
	
	// Update is called once per frame
	void Update () {
        UpdateHealthText();
    }
    //updates the health above object
    private void UpdateHealthText()
    {
        if( health > goodHealth)
        {
            healthText.text = "<color=#00FF00>" + health + "</color>";
        }
        else if(health > goodAvgHealth)
        {
            healthText.text = "<color=#7FFF00>" + health + "</color>";
        }
        else if (health > avgHealth)
        {
            healthText.text = "<color=#FFFF00>" + health + "</color>";
        }
        else if (health > badAvgHealth)
        {
            healthText.text = "<color=FFFF00>" + health + "</color>";
        }
        else if (health > badHealth)
        {
            healthText.text = "<color=#FF0000>" + health + "</color>";
        }
        else
        {
            healthText.text = "<color=red>" + health + "</color>";
        }

    }
    int IDamageable.getMaxHealth()
    {
        Debug.Log("Returned max healh of:" + maxHealth);
        return maxHealth;
    }

    int IDamageable.getHealth()
    {
        Debug.Log("Returned current healh of:" + health);
        return health;
    }

    void IDamageable.modifyHealth(int amount)
    {
        Debug.Log("Modifed health by:" + amount);

        health -= amount;
        Debug.Log("Current healh is now:" + health);
    }
}
