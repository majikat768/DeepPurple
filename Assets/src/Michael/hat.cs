using UnityEngine;

public class hat : MonoBehaviour {
    GameObject Player;
    Transform Head;
    float t = 0.0f;
    float speed = 3;
    Vector3 location;
    bool wearing = false;

    void Start() {
        Player = GameObject.FindWithTag("Player");
        Head = Player.transform.Find("Armature/VBOT_:Hips/VBOT_:Spine/VBOT_:Spine1/VBOT_:Spine2/VBOT_:Neck/VBOT_:Head/VBOT_:HeadTop_End");
        location = this.transform.position;

    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject == Player && !wearing) {
            this.transform.parent = Head;
            this.transform.localPosition = new Vector3(0,-5,-6);
            this.transform.localScale = new Vector3(100,100,100);
            this.transform.forward = Player.transform.forward;
            wearing = true;
        }
    }

    void Update() {
        if(!wearing) {
            //transform.position = location + new Vector3(0,Mathf.Sin(t),0)/8;
            transform.Rotate(0,2,0);
            //t += speed*Time.deltaTime;
        }
    }
}
