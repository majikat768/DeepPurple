/*	OK_SingletonScore.cs
 *	Name: Oshan Karki
 *	Description: This script sets dddScore for players using Singleton Pattern.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singleton
{
	public class SingletonScore : MonoBehaviour 
	{

		private static SingletonScore instance = new SingletonScore();

		public int playerScore;
		private LaserBeamLauncher laserBeamLauncher;

		private void Awake()
		{
			if(instance != null)
			{
				Debug.Log("Instance already created");
				return;
			}
			instance = this;
		}

		void Start()
		{
			laserBeamLauncher = GetComponent<LaserBeamLauncher>();
		}

		void Update()
		{
			playerScore = laserBeamLauncher.hitScore;
			print("Current Score is: "+ playerScore);
		}

		int getTotalScore()
		{
			return playerScore;
		}
	}

}