using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleOne : MonoBehaviour {

    private GameObject Player;
    private Bounds roomBounds;
    private Vector3 size;
    private Vector3 Zero;
    private Inventory inventory;
    public List<GameObject> Doors = new List<GameObject>();

	void Start () {
        inventory = GameObject.Find("GameManager").GetComponent<Inventory>();
        Player = GameObject.FindWithTag("Player");
        Doors = this.GetComponent<RoomGenerator>().MyDoors;
        roomBounds = new Bounds(new Vector3(Zero.x+size.x/2,Zero.y+size.y/2,Zero.z+size.z/2),size);

        GameObject key = GameObject.Instantiate(Resources.Load<GameObject>("Gabriel/Items/Interactable"),Zero+size/2,Quaternion.identity,this.transform);
		
	}
	
	void Update () {
        if(roomBounds.Contains(Player.transform.position)) {
            Debug.Log("locking doors");
            foreach(GameObject door in Doors) {
                if(door.GetComponent<OpenDoor>())
                    door.GetComponent<OpenDoor>().Lock();
            }
            this.gameObject.transform.Find("Ceiling").GetComponent<Renderer>().material.SetColor("_Color",new Color(0.0f,0.0f,0.0f,1.0f));
            this.gameObject.transform.Find("Floor").GetComponent<Renderer>().material.SetColor("_Color",new Color(0.0f,0.0f,0.0f,1.0f));
        }
		
        if(inventory.items.Count > 0) {
            Debug.Log("unlocking doors");
            foreach(GameObject door in Doors) {
                if(door.GetComponent<OpenDoor>())
                    door.GetComponent<OpenDoor>().Unlock();
            }
            this.gameObject.transform.Find("Ceiling").GetComponent<Renderer>().material.SetColor("_Color",new Color(0.0f,0.09f,0.39f,1.0f));
            this.gameObject.transform.Find("Floor").GetComponent<Renderer>().material.SetColor("_Color",new Color(0.0f,0.09f,0.39f,1.0f));
        }

	}

    public void SetBounds(Vector3 Zero, Vector3 size) {
        this.Zero = Zero;
        this.size = size;
    }
}
