using UnityEngine;

public class PuzzleOne : MonoBehaviour {

    private GameObject Player;
    private Inventory inventory;
    private bool solved;
    public Room R;

    public void Start()
    {
        Player = GameObject.FindWithTag("Player");

        inventory = GameObject.Find("GameManager").GetComponent<Inventory>();
        Player = GameObject.FindWithTag("Player");
    }

    public void Init(Vector3 Zero, Vector3 size)
    { 
        GameObject key = GameObject.Instantiate(
            Resources.Load<GameObject>("Gabriel/Items/GameObjects/Interactable"),
            Zero+size/2,Quaternion.identity,this.transform);
		
	}

	void Update () {
        if (!solved && inventory != null)
        {
           if (inventory.items.Count > 0)
            {
                this.solved = true;
            }
        }

	}
    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}
