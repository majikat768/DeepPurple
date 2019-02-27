using UnityEngine;

public class TreasureRoom : Room
{
    public TreasureRoom(Vector3 Zero) : base(Zero)
    {
        // the Treasure Room will have items or something; here, represented by green blocks.
        // Item i = new Item();
        GameObject items = new GameObject("Items");
        items.transform.parent = room.transform;
        for (int i = 0; i < size / 2; i++)
        {
            GameObject b = GameObject.Instantiate(Block, new Vector3(Zero.x + Random.Range(1, size - 1) + 0.5f, 0.5f, Zero.z + Random.Range(1, size - 1) + 0.5f), Quaternion.identity);
            b.transform.parent = items.transform;
        }
        Debug.Log("Treasure");
    }
}
