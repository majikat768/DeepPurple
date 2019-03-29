using UnityEngine;

public class TreasureRoom : Room
{
    private readonly int numItems = 4;
    private readonly int numGold = 4;
    GameObject items;

    private GameObject Interactable;
    private GameObject Currency; 
    private GameObject Chest;

    public void Start()
    {
        Interactable = Resources.Load<GameObject>("Gabriel/Items/GameObjects/Interactable");
        Currency = Resources.Load<GameObject>("Gabriel/Items/GameObjects/CurrencyItem");
        Chest = Resources.Load<GameObject>("Gabriel/Chest_GameObject");


        // Item i = new Item();
        items = new GameObject("Items");
        GameObject chest = GameObject.Instantiate(Chest,Zero + size/2,Quaternion.Euler(-90,0,0),items.transform);
        Collider[] chestCollisions = Physics.OverlapBox(chest.GetComponent<Renderer>().bounds.center,chest.GetComponent<Renderer>().bounds.size);
        for(int i = 0; i < chestCollisions.Length; i++) {
            if(chestCollisions[i].name == "Wall")
                Destroy(chestCollisions[i].gameObject);
        }

        /*
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
        */

    }
}
