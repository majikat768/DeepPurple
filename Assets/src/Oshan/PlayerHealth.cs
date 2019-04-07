/*	OK_PlayerHealth.cs
 *	Name: Oshan Karki
 *	Description: This script keeps track of player health.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour 
{

	public int playerHealth = 10;
	int damage = 2;

	void Start()
	{
		print(playerHealth);
	}

	public void setHealth()
	{
		playerHealth += 2;
	}

	void OnCollisionEnter(Collision _collision)
	{

		if(_collision.gameObject.tag == "Enemy")
		{
			playerHealth -= damage;
			print("Enemy just touched me, please help " + playerHealth);

			if(playerHealth == 0)
			{
				print("Your player just died");
				return;
			}
		}

		if(_collision.gameObject.tag == "healthPotion")
		{
			setHealth();
		}
	}


	public int getPlayerHealth()
	{
		return playerHealth;
	}

}
