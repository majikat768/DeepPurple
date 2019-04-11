using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {
    ParticleSystem ps;
    GameObject Player;

    float speedStep = 0.01f;

    void Start() {
        ps = this.GetComponent<ParticleSystem>();
        ps.Stop();
        Player = GameObject.FindWithTag("Player");
    }

    void Update() {
        if(ps.isPlaying) {
            this.transform.Rotate(0,0,2);
            //this.transform.position += new Vector3(0,0,Mathf.Sin(Time.realtimeSinceStartup)*Time.deltaTime/2);
            var main = ps.main;
            main.simulationSpeed += speedStep * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject == Player) {
            SceneManager.LoadScene(0);
        }
    }
}
