using UnityEngine;

//This was simply a test puzzle, to see if I could make it lock/unlock doors based on a condition.
//If the player's inventory is empty, doors are locked;
// once the player picks up an item in the middle of the room, doors unlock.
//
public class PuzzleOne : MonoBehaviour {

    private Inventory inventory;
    private bool solved;
    private Vector3 Zero, size;
    public PuzzleRoom R;

    public void Start()
    {

        inventory = GameObject.Find("GameManager").GetComponent<Inventory>();
        R = this.GetComponent<PuzzleRoom>();
        Zero = R.GetZero();
        size = R.GetSize();

        GameObject.Instantiate(
            Resources.Load<GameObject>("Gabriel/Items/GameObjects/Interactable"),
            Zero+size/2,Quaternion.identity,this.transform);
		
	}

	void Update () {
        if (!R.solved)
        {
            if (inventory != null)
            {
                if (inventory.items.Count > 0)
                {
                    R.solved = true;
                    R.PlaySolvedSound();
                }
            }
        }

	}
    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}
