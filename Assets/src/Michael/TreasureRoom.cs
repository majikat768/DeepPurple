using UnityEngine;

public class TreasureRoom : Room
{
    private int numItems = 6;
    private int numGold = 8;
    GameObject items;

    private GameObject Interactable = Resources.Load<GameObject>("Gabriel/Items/Interactable");
    private GameObject Currency = Resources.Load<GameObject>("Gabriel/Items/CurrencyItem");

    public TreasureRoom(Vector3 Zero) : base(Zero)
    {
        // Item i = new Item();
        items = new GameObject("Items");
        Vector3 SpawnPoint;
        items.transform.parent = room.transform;

        for (int i = 0; i < numItems; i++)
        {
            SpawnPoint = new Vector3(
                    Zero.x + Random.Range(0, size.x - 1), 
                    1.5f, 
                    Zero.z + Random.Range(0, size.z - 1));

            GameObject item = Object.Instantiate(Interactable,SpawnPoint,Quaternion.identity) as GameObject;
            item.transform.parent = items.transform;
        }

        for(int i = 0; i < numGold; i++) {
            SpawnPoint = new Vector3(
                    Zero.x + Random.Range(1,size.x-2),
                    1.5f,
                    Zero.z + Random.Range(1,size.z-2));

            GameObject currency = Object.Instantiate(Currency,SpawnPoint,Quaternion.identity,items.transform);
        }

        Debug.Log("Treasure");
    }
}
