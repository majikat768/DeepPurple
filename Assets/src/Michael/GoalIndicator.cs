using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GoalIndicator : MonoBehaviour {
    Camera cam;
    GameObject boss;
    Vector3 bossPosition;
    Vector3 screenMiddle;
    Canvas arrowCanvas;
    Image arrow;

    void Start () {
        cam = Camera.main;
        boss = GameObject.Find("Boss");
        arrowCanvas = this.gameObject.AddComponent<Canvas>();
        arrowCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        arrow = this.gameObject.GetComponent<Image>();
        
    }

    void Update() {
        if(boss == null) boss = GameObject.Find("Boss");
        bossPosition = cam.WorldToScreenPoint(boss.transform.position);
        screenMiddle = new Vector3(Screen.width/2,Screen.height/2,0);
        var tarAngle = (Mathf.Atan2(bossPosition.x-screenMiddle.x,Screen.height-bossPosition.y-screenMiddle.y) * Mathf.Rad2Deg)+90;
        if (tarAngle < 0) tarAngle +=360;

        var bossDir = boss.transform.position - cam.transform.position;
        if(Vector3.Angle(bossDir,cam.transform.forward) < 90) {
            arrow.transform.localRotation = Quaternion.Euler(-tarAngle,90,270);
        } 
        else {
         arrow.transform.localRotation = Quaternion.Euler(tarAngle,270,90);
        }
    }

}
