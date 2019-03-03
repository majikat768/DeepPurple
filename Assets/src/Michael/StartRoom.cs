using UnityEngine;

public class StartRoom : Room
{
    public GameObject Player;
    public StartRoom(Vector3 Zero,GameObject r) : base(Zero,r)
    {
        // the Start Room will have the player character spawn in it.
        // Player player = new Player();
        // Here, player represented by purple block
        // 
        Vector3 SpawnPoint = new Vector3(size.x/2,0.5f,1.0f);
        /*
            Player = Object.Instantiate(FirstPerson, SpawnPoint, Quaternion.identity, room.transform);
        else
            Player = Object.Instantiate(PlayerBall, SpawnPoint, Quaternion.identity, room.transform);
        Player.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
        Player.transform.parent = room.transform;
        Player.tag = "Player";
        Debug.Log("start");
        */
    }

}
