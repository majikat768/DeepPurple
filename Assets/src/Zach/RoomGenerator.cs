using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject[] roomArray;
    // Start is called before the first frame update
    void Start()
    {
        int difficulty = Registry.GetOrDefault<int>("difficulty", 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
