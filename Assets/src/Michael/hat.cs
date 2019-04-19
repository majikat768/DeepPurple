using UnityEngine;

public class hat : MonoBehaviour {
    GameObject Player;
    Transform Head;
    bool wearing = false;

    void Start() {
        Player = GameObject.FindWithTag("Player");
        Head = Player.transform.Find("Armature/VBOT_:Hips/VBOT_:Spine/VBOT_:Spine1/VBOT_:Spine2/VBOT_:Neck/VBOT_:Head/VBOT_:HeadTop_End");

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
}
