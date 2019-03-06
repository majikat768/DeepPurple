using UnityEngine;

public class Interactable : MonoBehaviour {

	// how close the player needs to come in order to interact with an object
	public float radius = 3f;
	public float offset_X;
	public float offset_Y;
	public float offset_Z;
	Transform player;

	// virtual means function can be overwritten from other subclasses
	public virtual void Interact()
	{
		// This method is meant to be overwritten

		Debug.Log("Interacting with " + transform.name);
	}

	void Update()
	{
		float distance = Vector3.Distance(player.position, transform.position);
		if(distance <= radius)
		{
			Interact();
		}
	}

	void Start()
	{
        //GameObject playerCharacter = GameObject.Find("RollerBall");
        GameObject playerCharacter = GameObject.FindWithTag("Player");
		player = playerCharacter.transform;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Vector3 positionWire = new Vector3(transform.position.x + offset_X, transform.position.y + offset_Y, transform.position.z + offset_Z);
		Gizmos.DrawWireSphere(positionWire, radius);
	}



}
