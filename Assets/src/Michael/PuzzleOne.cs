using UnityEngine;

public class PuzzleOne : MonoBehaviour {

    private GameObject Player;
    private Inventory inventory;
    private bool solved;
    private Vector3 Zero, size;
    public PuzzleRoom R;

    public void Start()
    {
        Player = GameObject.FindWithTag("Player");

        inventory = GameObject.Find("GameManager").GetComponent<Inventory>();
        R = this.GetComponent<PuzzleRoom>();
        Zero = R.GetZero();
        size = R.GetSize();

        GameObject key = GameObject.Instantiate(
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
