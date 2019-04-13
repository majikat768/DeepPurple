using UnityEngine;

public class StartRoom : Room
{
    protected override void Start()
    {

        // the Start Room will have the player character spawn in it.
        // Player player = new Player();
        // Here, player represented by purple block
        // 
        Vector3 SpawnPoint = new Vector3(Zero.x+size.x / 2, 2.0f, Zero.z+size.z / 2);
        Player = GameObject.FindWithTag("Player");
        //SetLighting(RoomGenerator.Amber,2);
        Collider[] playerCollisions = Physics.OverlapBox(SpawnPoint,new Vector3(1,1,1));
        for(int i = 0; i < playerCollisions.Length; i++)
        {
            if(playerCollisions[i].name == "Wall")
            {
                SpawnPoint = new Vector3(Zero.x+Random.Range(1,size.x-2), 2.0f, Zero.z+Random.Range(1,size.z-2));
                playerCollisions = Physics.OverlapBox(SpawnPoint,new Vector3(1,1,1));
                i = -1;
            }
        }
        Player.transform.position = SpawnPoint;
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
