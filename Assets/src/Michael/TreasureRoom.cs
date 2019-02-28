using UnityEngine;

public class TreasureRoom : Room
{
    private int numItems = 8;
    public TreasureRoom(Vector3 Zero) : base(Zero)
    {
        // the Treasure Room will have items or something; here, represented by green blocks.
        // Item i = new Item();
        GameObject items = new GameObject("Items");
        items.transform.parent = room.transform;
        for (int i = 0; i < numItems; i++)
        {
            GameObject b = GameObject.Instantiate(Block, new Vector3(Zero.x + Random.Range(1, size.x - 1) + 0.5f, 0.5f, Zero.z + Random.Range(1, size.z - 1) + 0.5f), Quaternion.identity);
            b.transform.parent = items.transform;
            b.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.0f, 0.8f, 0.0f, 1.0f));
        }
        Debug.Log("Treasure");
    }
}
