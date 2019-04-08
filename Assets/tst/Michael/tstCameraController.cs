using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tstCameraController : MonoBehaviour
{
    public float FollowDistance = 5;
    public float MouseSensitivity = 0.1f;
    public float height = 2.0f;
    private Vector3 direction;
    private GameObject player;
    private float MouseX, MouseY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        this.transform.LookAt(player.transform);
        direction = this.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        direction = (direction + new Vector3(0,MouseY*MouseSensitivity,0)+this.transform.right*MouseX*MouseSensitivity).normalized;

        this.transform.position = player.transform.position - direction*FollowDistance + new Vector3(0,height,0);
        this.transform.LookAt(player.transform);
    }
}
