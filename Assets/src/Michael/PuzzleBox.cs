using UnityEngine;

// spawns a box, and a target tile.
// Player must move the box on to the target tile to solve the room.
//

public class PuzzleBox : PuzzleRoom {

    private GameObject box;
    private GameObject TargetTile;
    private GameObject FloorTile;

    protected void Awake() {
        base.Awake();
        instructions = "move the box to the target";
        TimeLimit = 30;
        //this.GetComponent<Room>().complexity = -1;
    }

    protected void Start()
    {
        FloorTile = RG.FloorTile;

        Vector3 SpawnPoint = Zero + new Vector3(Random.Range(2, size.x-3), size.y / 2, Random.Range(2, size.z-3));
        box = GameObject.Instantiate(
            Resources.Load<GameObject>("Michael/Crate_003"),
            SpawnPoint,
            Quaternion.Euler(-90,0,0),
            this.transform);
        Collider[] boxCollisions = Physics.OverlapBox(box.GetComponent<Collider>().bounds.center,box.GetComponent<Collider>().bounds.size);
        for(int i = 0; i < boxCollisions.Length; i++) {
            if(boxCollisions[i].name == "Wall")
            {
                SpawnPoint = Zero + new Vector3(Random.Range(2, size.x-3), size.y / 2, Random.Range(2, size.z-3));
                box.transform.position = SpawnPoint;
                boxCollisions = Physics.OverlapBox(box.GetComponent<Collider>().bounds.center,box.GetComponent<Collider>().bounds.size/2);
                i = -1;
            }
        }

        SpawnPoint = Zero + new Vector3(Random.Range(2, size.x-3), FloorTile.GetComponent<Renderer>().bounds.size.y/2, Random.Range(2, size.z-3));
        TargetTile = GameObject.Instantiate(FloorTile, SpawnPoint, Quaternion.identity, this.gameObject.transform);

        TargetTile.GetComponent<Renderer>().materials[0].color = new Color(0.31f, 0.98f, 0.16f);
        Destroy(TargetTile.GetComponent<Collider>());
        TargetTile.transform.localScale *= 1.5f;
        TargetTile.name = "Target";
		
	}

    // The box also repels walls, so it won't get stuck in a corner;
    // and after it's solved, the box turns green
    // and  always moves towards the target, giving it a magnetic like effect.

	protected override void Update () {
        if (!solved) 
        {
            foreach(Collider o in Physics.OverlapBox(box.GetComponent<Renderer>().bounds.center,box.GetComponent<Renderer>().bounds.size))
            {
                if (o.name == "Wall")
                {
                    Vector3 dir = (box.transform.position - o.ClosestPoint(box.transform.position)).normalized;
                    box.GetComponent<Rigidbody>().MovePosition(box.transform.position + dir * Time.deltaTime);
                }
            }

            if (Mathf.Abs(box.transform.position.x - GetZero().x) < 2.0f || 
                Mathf.Abs(box.transform.position.x - (GetZero().x+GetSize().x)) < 2.0f ||
                Mathf.Abs(box.transform.position.z - GetZero().z) < 2.0f ||
                Mathf.Abs(box.transform.position.z - (GetZero().z+GetSize().z)) < 2.0f)
            {
                Vector3 dir = (GetZero() + GetSize() / 2 - box.transform.position).normalized;
                box.GetComponent<Rigidbody>().MovePosition(box.transform.position + (dir * Time.deltaTime));
            }
        }

        if(solved && Vector3.Distance(box.transform.position,TargetTile.transform.position) > 0.1f)
        {
            Vector3 dir = (TargetTile.transform.position-box.transform.position).normalized;
            box.GetComponent<Rigidbody>().MovePosition(box.transform.position+(dir*Time.deltaTime));
        }

	}

    protected override void CheckSolveConditions() {
        if (Vector3.Distance(box.transform.position, TargetTile.transform.position) < 1.0f)
        {
            solved = true;
            PlaySolvedSound();
            box.GetComponent<Renderer>().materials[0].color = new Color(0.6f, 0.8f, 0.2f);
        }
    }
    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}
