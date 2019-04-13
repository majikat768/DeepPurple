using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLasers : PuzzleRoom {

    int numLasers = 9;
    List<GameObject> Lasers;
    Vector3 CenterOfRoom;

    new void Awake() {
        base.Awake();
        complexity = 1;
        Lasers = new List<GameObject>();
    }

    public void Start() {
        CenterOfRoom = Zero+size/2;

        for(int i = 0; i < numLasers; i++) {
            GameObject w = R.Walls.transform.GetChild(Random.Range(0,R.Walls.transform.childCount-1)).gameObject;
            CreateLaser(w);
        }
    }

    void CreateLaser(GameObject w) {
        GameObject l = new GameObject("laser");
        l.transform.position = w.transform.position + 
            (w.GetComponent<Renderer>().bounds.size.x > w.GetComponent<Renderer>().bounds.size.z ?
             new Vector3(Random.Range(-w.GetComponent<Renderer>().bounds.size.x,w.GetComponent<Renderer>().bounds.size.x)/3,0,0) : 
             new Vector3(0,0,Random.Range(-w.GetComponent<Renderer>().bounds.size.z,w.GetComponent<Renderer>().bounds.size.z)/3));
        l.transform.position = Vector3.MoveTowards(l.transform.position,CenterOfRoom,1);
        l.transform.parent = this.transform;

        ParticleSystem lps = l.AddComponent<ParticleSystem>();
        lps.Stop();
        ParticleSystemRenderer lpsr = l.GetComponent<ParticleSystemRenderer>();
        var main = lps.main;
        main.duration = 1;
        main.startSpeed = new ParticleSystem.MinMaxCurve(-0.1f,0.1f);
        main.startColor = new Color(1,0,0);
        main.startLifetime = 0.5f;
        main.startSize = 0.1f;
        main.maxParticles = 5000;

        lps.emissionRate = 5000;
        lpsr.material = Resources.Load<Material>("Michael/Materials/ParticleGlow");

        var sh = lps.shape;
        sh.shapeType = ParticleSystemShapeType.ConeVolume;
        sh.randomDirectionAmount = 1;
        sh.angle = 0;
        sh.radius = 0.01f;
        sh.radiusThickness = 0;
        Lasers.Add(l);
        lps.Play();
        l.transform.LookAt(CenterOfRoom);
        l.transform.Rotate(new Vector3(0,Random.Range(10,30)*(Random.value > 0.5f ? -1 : 1),0));
        RaycastHit hit;
        if(Physics.Raycast(l.transform.position,l.transform.forward,out hit,Mathf.Infinity,RoomGenerator.WallMask)) {
            while(hit.transform.name == "Door") {
                l.transform.LookAt(CenterOfRoom);
                l.transform.Rotate(new Vector3(0,Random.Range(10,30)*(Random.value > 0.5f ? -1 : 1),0));
                Physics.Raycast(l.transform.position,l.transform.forward,out hit,Mathf.Infinity,RoomGenerator.WallMask);
            }
            sh.length = Vector3.Distance(l.transform.position,hit.point);
        }
    }
}

