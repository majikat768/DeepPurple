using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerMovement : MonoBehaviour
{
    public float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //print("hello I am a main player");
        MoveSpeed = 7.5f;

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(1f*Time.deltaTime, 0f, 0f);
        transform.position += new Vector3(MoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
    }
}
