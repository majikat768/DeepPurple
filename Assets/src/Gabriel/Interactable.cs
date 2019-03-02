using UnityEngine;

public class Interactable : MonoBehaviour {

	// how close the player needs to come in order to interact with an object
	public float radius = 3f;
	
	Transform player;

	bool hasInteracted = false;

	// virtual means function can be overwritten from other subclasses
	public virtual void Interact()
	{
		// This method is meant to be overwritten

		Debug.Log("Interacting with " + transform.name);
	}

	void Update()
	{
		if(hasInteracted == false)
		{
			float distance = Vector3.Distance(player.position, transform.position);
			if(distance <= radius)
			{
				hasInteracted = true;
				Interact();
			}
		}
	}

	void Start()
	{
		GameObject playerCharacter = GameObject.Find("RollerBall");
		player = playerCharacter.transform;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, radius);
	}



}
