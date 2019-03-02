using UnityEngine;

public class TreasureRoom : Room
{
    private int numItems = 8;
    public TreasureRoom(Vector3 Zero) : base(Zero)
    {
        // the Treasure Room will have items or something; here, represented by green blocks.
        // Item i = new Item();
        GameObject items = new GameObject("Items");
        Vector3 SpawnPoint;
        items.transform.parent = room.transform;
        for (int i = 0; i < numItems; i++)
        {
            SpawnPoint = new Vector3(Zero.x + Random.Range(0, size.x - 1), 1.5f, Zero.z + Random.Range(0, size.z - 1));
            GameObject b = GameObject.Instantiate(Block, SpawnPoint, Quaternion.identity);
            b.transform.localScale = new Vector3(Random.Range(0.5f, 2.0f), Random.Range(0.5f,2.0f), Random.Range(0.5f, 2.0f));
            b.transform.parent = items.transform;
            //b.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            b.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.0f, 0.8f, 0.0f, 1.0f));
        }
        Debug.Log("Treasure");
    }
}
