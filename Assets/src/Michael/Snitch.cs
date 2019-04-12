using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class Snitch : MonoBehaviour {

    PuzzleLowGravity R;
    static Inventory inventory;
    GameObject player;
    Vector3 target,Zero,size;
    TextMeshProUGUI points;
    float speed = 0.1f;

	// Use this for initialization
	void Start () {
        points = GameObject.Find("HUD").GetComponent<InventoryUI>().textMoney;
        inventory = Inventory.instance;
	    R = this.transform.parent.GetComponent<PuzzleLowGravity>();	
        Zero = R.GetZero();
        size = R.GetSize();
        player = GameObject.FindWithTag("Player");
        target = Zero+new Vector3(Random.Range(1,size.x-2),Random.Range(R.Floor.transform.position.y,R.Ceiling.transform.position.y),Random.Range(1,size.z-2));
	}
	
	// Update is called once per frame
	void Update () {
        if(R.PlayerInRoom) {
            if(Vector3.Distance(this.transform.position,target) < 1) {
                target = Zero+new Vector3(Random.Range(1,size.x-2),Random.Range(R.Floor.transform.position.y,R.Ceiling.transform.position.y),Random.Range(1,size.z-2));
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position,target,speed);
        }
		
	}

    void OnTriggerEnter(Collider other) {
        if(other.gameObject == player) {
            R.SnitchList.Remove(this.gameObject);
            inventory.money++;
            points.text = inventory.money.ToString();
            Destroy(this.gameObject);
            R.PlaySolvedSound();
        }
    }
}
