/*	OK_DecorativeHealth
 *	Name: Oshan Karki
 *	Description: This heath system calculates player health and sends it to UI
 				 If player reaches zero then the player game object is destroyed.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecorativeHealth : MonoBehaviour 
{

	public int playerHealth = 100;
	int damage = 10;

	public virtual void addHealth(int hp)
	{
		playerHealth += hp;
	}

	void OnCollisionEnter(Collision _collision)
	{
		if(_collision.gameObject.tag == "Enemy")
		{
			playerHealth -= damage;
			print("Health decreased by "+ damage);
			if(playerHealth == 0)
			{
				print("You Lose");
				return;			// Either Destroy object or use Dead animation.
			}
		}
	}

}

public class UsePotion: DecorativeHealth
{
	public int potionHp = 30;
/*
	public override void addHealth()
	{
		if(playerHealth != 100)
		{
			playerHealth += potionHp;
		}
	}
	*/

	void OnCollisionEnter(Collision _collision)
	{
		if(_collision.gameObject.tag == "HealthPotion")
		{
			addHealth(potionHp);
		}
	}

}
