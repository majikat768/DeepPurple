using UnityEngine;

public class StartRoom : Room
{
    public StartRoom(Vector3 Zero) : base(Zero)
    {
        // the Start Room will have the player character spawn in it.
        // Player player = new Player();
        // Here, player represented by purple block
        // 
        GameObject Player = GameObject.Instantiate(Block, new Vector3(size/2, 0.5f, 1.0f), Quaternion.identity, room.transform);
        Player.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
        Player.transform.parent = room.transform;
        Debug.Log("start");
    }

}
