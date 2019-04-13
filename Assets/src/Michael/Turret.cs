using UnityEngine;
using System.Collections.Generic;

public class Turret : MonoBehaviour {
    float scanSpeed;
    ParticleSystem laser;

    public void Start() {
        laser = GetComponent<ParticleSystem>();
        scanSpeed =20;
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
    }

    public void Target() {
        var main = laser.main;
        var shape = laser.shape;
        main.startLifetime = 1;
        main.startSpeed = 50;
        shape.shapeType = ParticleSystemShapeType.Cone;
        main.startColor = new Color(1,0,0);
    }

}
