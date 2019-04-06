using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

//basically casts a Ray between the camera and the player, and if any Wall objects intersect this ray, 
//make them transparent.
//

public class WallTransparency : MonoBehaviour {

    Material[] mats;
    Color col;
    Camera cam;
    GameObject player,w;
    List<GameObject> hits;
    float FadeSpeed = 2;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        hits = new List<GameObject>();
        player = GameObject.FindWithTag("Player");
        mats = this.GetComponent<Renderer>().materials;
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if(player == null)
            player = GameObject.FindWithTag("Player");
        hits.Clear();
        Vector3 dir = player.transform.position - cam.transform.position;
        Debug.DrawLine(cam.transform.position,player.transform.position,Color.white,0.1f);

        foreach(RaycastHit hit in Physics.RaycastAll(cam.transform.position,dir,Mathf.Infinity)) 
            hits.Add(hit.transform.gameObject);

        if(hits.Contains(this.gameObject)) {
            FadeOut();
        }
        else {
            FadeIn();
        }

	}

    // I have no idea what any of these functions do.
    // but apparently they're necessary for changing a material's rendering mode from
    // Opaque to Transparent, and vice versa.
    //
    // After rendering mode is changed, just change material's alpha value incrementally till it's faded out/in.
    //
    private void FadeOut() {
        foreach(Material mat in mats) {
        col = mat.color;
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;

        mat.SetFloat("_Mode",3f);
        if(col.a > 0.00f)    col.a -= FadeSpeed * Time.deltaTime;
        mat.SetColor("_Color",col);
        }
    }

    private void FadeIn() 
    {
        foreach(Material mat in mats) {
            col = mat.color;
            if(col.a >= 0.9f) 
            {
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = -1;
                mat.SetFloat("_Mode",0f);
            }

            if(col.a < 1.0f)    col.a += FadeSpeed * Time.deltaTime;
            mat.SetColor("_Color",col);
        }
    }

}
