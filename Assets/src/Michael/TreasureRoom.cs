using UnityEngine;

public class TreasureRoom : Room
{
    private readonly int numItems = 4;
    private readonly int numGold = 4;
    GameObject items;

    private GameObject Interactable;
    private GameObject Currency; 

    public void Start()
    {
        Interactable = Resources.Load<GameObject>("Gabriel/Items/GameObjects/Interactable");
        Currency = Resources.Load<GameObject>("Gabriel/Items/GameObjects/CurrencyItem");


        // Item i = new Item();
        items = new GameObject("Items");
        Vector3 SpawnPoint;
        items.transform.parent = this.transform;

        for (int i = 0; i < numItems; i++)
        {
            SpawnPoint = new Vector3(
                    Zero.x + Random.Range(0, size.x - 1), 
                    1.5f, 
                    Zero.z + Random.Range(0, size.z - 1));

            Object.Instantiate(Interactable,SpawnPoint,Quaternion.identity,items.transform);
        }

        for(int i = 0; i < numGold; i++) {
            SpawnPoint = new Vector3(
                    Zero.x + Random.Range(1,size.x-2),
                    1.5f,
                    Zero.z + Random.Range(1,size.z-2));

            Object.Instantiate(Currency,SpawnPoint,Quaternion.identity,items.transform);
        }

    }
}
