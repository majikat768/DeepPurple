using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpening : Interactable {

	public Vector3 scale;
	public float durationTime;
	public GameObject confetti;
	public GameObject item;

	public override void Interact()
	{
		// calls base interact method
		base.Interact();

		chestOpenCall(scale, durationTime);
	}

	public virtual void Spawn()
	{
		Debug.Log("Spawning");
		//Example for how spawning will work
		//DONT ACTUALLY DO THIS JUST SPAWN LIKE ONE ITEM 
		/*
		Vector3 currentPosition = gameObject.transform.position;
		currentPosition.y = currentPosition.y + 1.5f;
		currentPosition.z = currentPosition.z + Random.Range(-1.0f,1.0f);
		currentPosition.x = currentPosition.x + Random.Range(-0.7f,0.7f);
		Rigidbody clone = Instantiate(item.GetComponent<Rigidbody>(), currentPosition, gameObject.transform.rotation);
		clone.velocity = new Vector3(Random.Range(-2f,2f),5,Random.Range(-2f,2f));
		*/
	}

	public void chestOpenCall(Vector3 targetScale, float duration)
	{
		//Spawn Items here
		Spawn();
		//Graphics for opening the chest
		Vector3 currentPosition = gameObject.transform.position;
		currentPosition.y = currentPosition.y + 1.5f;
		currentPosition.z = currentPosition.z + Random.Range(-1.0f,1.0f);
		currentPosition.x = currentPosition.x + Random.Range(-0.7f,0.7f);
		Rigidbody clone = Instantiate(confetti.GetComponent<Rigidbody>(), currentPosition, gameObject.transform.rotation);
		clone.velocity = new Vector3(Random.Range(-2f,2f),5,Random.Range(-2f,2f));
		//Start destroying Coroutine
		StartCoroutine(ScaleToTargetCo(targetScale, duration));
	}

    private IEnumerator ScaleToTargetCo(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float timer = 0.0f;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            t = t * t* t * (t * (6f * t - 15f) + 10f);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
 
        yield return null;
		Destroy(this.gameObject);
    }
}
