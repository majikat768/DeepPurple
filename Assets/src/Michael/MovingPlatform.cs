using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    GameObject player;
    public float speed = 1;
    public bool moving = true;
    public bool playerOnPlatform = false;
    public bool OnlyMoveWithPlayer = false;
    public Vector3 home,start,end,direction;
    Bounds platformBounds;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        start = this.transform.position;
        home = start;
        if(OnlyMoveWithPlayer)  moving = false;
        platformBounds = this.GetComponent<Renderer>().bounds;
	}
	
	void FixedUpdate () {
        if(end == Vector3.zero) {
            end = new Vector3(start.x,start.y*3,start.z);
        }
        if(direction == Vector3.zero)
            direction = (end - start).normalized;
        if(moving) {
            this.transform.position += direction * speed * Time.deltaTime;
            foreach(Collider o in Physics.OverlapBox(platformBounds.center,platformBounds.size/2)) {
                Debug.Log(o.name);
                if(o.name == "Item") {
                   o.transform.position = this.transform.position + new Vector3(0,platformBounds.size.y,0)*2;
                }
            }
            if(playerOnPlatform) player.transform.position += direction * speed * Time.deltaTime;
            //this.transform.position = Vector3.Lerp(this.transform.position,end,speed * Time.deltaTime);
            if(Vector3.Distance(this.transform.position,end) < 0.2f) {
                Vector3 s = end;
                end = start;
                start = s;
                direction = (end - start).normalized;
            }
        }
        else if(Vector3.Distance(this.transform.position,home) > 0.1f)
            this.transform.position += (home-this.transform.position).normalized*speed*Time.deltaTime;
	}

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject == player) {
            if(OnlyMoveWithPlayer)
                moving = true;
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject == player) {
            if(OnlyMoveWithPlayer)
                moving = false;
            playerOnPlatform = false;
        }
    }

    public void Init(Vector3 home, Vector3 end, float speed = 1, bool OnlyMoveWithPlayer = false) {
        this.home = home;
        this.end = end;
        this.speed = speed;
        this.OnlyMoveWithPlayer = OnlyMoveWithPlayer;
    }

}
