using UnityEngine;
using System.Collections;

public class JumpPad : MonoBehaviour {
    GameObject Player;
    Rigidbody playerRB;
    public float bounciness;
    public Transform target;
    Vector3 home;
    Vector3 g;
    float speed = 8;

    void Start() {
        Player = GameObject.FindWithTag("Player");
        playerRB = Player.GetComponent<Rigidbody>();
        g = Physics.gravity;
        home = this.transform.position;

        this.GetComponent<Renderer>().materials[0].mainTexture = Resources.Load<Texture>("Michael/Materials/Wall03");
        this.GetComponent<Renderer>().materials[1].mainTexture = Resources.Load<Texture>("Michael/Materials/Wall03");
    }


    void OnTriggerStay(Collider other) {
        if(other.gameObject == Player) {
            playerRB.velocity = Vector3.zero;
            Vector3 dir = (target.position-this.transform.position);
            float dist = Vector3.Distance(this.transform.position,target.GetComponent<Collider>().ClosestPoint(Player.transform.position));
            playerRB.AddForce(dir*0.75f-4*g/3,ForceMode.VelocityChange);
            Debug.Log("bounce " + (dir-g).ToString());
        } 
    } 

}

