using UnityEngine;
using System.Collections.Generic;

public class Turret : MonoBehaviour {
    float scanSpeed;
    ParticleSystem laser;
    AudioClip scanner, fire;
    AudioSource audioSource;
    bool seesPlayer = false;
    public bool isDead = false;
    float KillTime; 
    float KillCounter; 

    public void Start() {
        KillTime = 2;
        KillCounter = 0;
        laser = GetComponent<ParticleSystem>();
        scanSpeed = 30;
        audioSource = this.GetComponent<AudioSource>();
        audioSource.pitch = 1.5f;
        scanner = Resources.Load<AudioClip>("Michael/Audio/LaserScan");
        fire = Resources.Load<AudioClip>("Michael/Audio/HeavyLaser");

    }

    void FixedUpdate() {
    }

    public void ScanRoom(RaycastHit hit) {
        var main = laser.main;
        var shape = laser.shape;
        main.startLifetime = 1;
        main.startSpeed = 0;
        main.startColor = new Color(0,1,0);
        shape.shapeType = ParticleSystemShapeType.ConeVolume;
        shape.length = Vector3.Distance(hit.point,laser.transform.position);
        transform.Rotate(0,scanSpeed*Time.deltaTime,0);
        if(seesPlayer) {
            audioSource.Stop();
            seesPlayer = false;
        }
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(scanner,0.5f);
        if(KillCounter > KillTime) {
            isDead = true;
        }
    }

    public void Target() {
        var main = laser.main;
        var shape = laser.shape;
        main.startLifetime = 1;
        main.startSpeed = 50;
        shape.shapeType = ParticleSystemShapeType.Cone;
        main.startColor = new Color(1,0,0);
        if(!seesPlayer) {
            audioSource.Stop();
            seesPlayer = true;
        }
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(fire,1.0f);
        if(KillCounter > KillTime) {
            isDead = true;
        }
    }

    private void OnParticleCollision(GameObject p) {
        if(p.name == "laserBeam") {
            this.KillCounter += Time.deltaTime;
            ParticleSystem Smoke = this.transform.Find("Smoke").GetComponent<ParticleSystem>();
            if(!Smoke.isPlaying)    Smoke.Play();
        }
    }
}
