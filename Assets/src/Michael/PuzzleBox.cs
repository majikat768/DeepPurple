using UnityEngine;

// spawns a box, and a target tile.
// Player must move the box on to the target tile to solve the room.
//

public class PuzzleBox : PuzzleRoom {

    private GameObject box;
    private GameObject TargetTile;
    private GameObject FloorTile;

    public void Awake() {
        base.Awake();
        //this.GetComponent<Room>().complexity = -1;
    }

    public void Start()
    {
        base.Start();
        FloorTile = RoomGenerator.FloorTile;

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
        /*
        TargetTile = R.FloorTiles[Random.Range(0,R.FloorTiles.Count)];
        while(Mathf.Abs(TargetTile.transform.position.x-Zero.x) < 3.0f || 
                Mathf.Abs(TargetTile.transform.position.x-(Zero.x+size.x)) < 3.0f ||
                Mathf.Abs(TargetTile.transform.position.z-Zero.z) < 3.0f || 
                Mathf.Abs(TargetTile.transform.position.z-(Zero.z+size.z)) < 3.0f)
        TargetTile = R.FloorTiles[Random.Range(0,R.FloorTiles.Count)];
        */

        TargetTile.GetComponent<Renderer>().materials[0].color = new Color(0.31f, 0.98f, 0.16f);
        Destroy(TargetTile.GetComponent<Collider>());
        TargetTile.transform.localScale *= 1.5f;
        TargetTile.name = "Target";
		
	}

    // The box also repels walls, so it won't get stuck in a corner;
    // and after it's solved, the box turns green
    // and  always moves towards the target, giving it a magnetic like effect.

	void FixedUpdate () {
        if (!R.solved) 
        {
            if (Vector3.Distance(box.transform.position, TargetTile.transform.position) < 1.0f)
            {
                R.solved = true;
                R.PlaySolvedSound();
                box.GetComponent<Renderer>().materials[0].color = new Color(0.6f, 0.8f, 0.2f);
            }

            foreach(Collider o in Physics.OverlapBox(box.GetComponent<Renderer>().bounds.center,box.GetComponent<Renderer>().bounds.size))
            {
                if (o.name == "Wall")
                {
                    Vector3 dir = (box.transform.position - o.ClosestPoint(box.transform.position)).normalized;
                    box.GetComponent<Rigidbody>().MovePosition(box.transform.position + dir * Time.deltaTime);
                }
            }

            if (Mathf.Abs(box.transform.position.x - R.GetZero().x) < 2.0f || 
                Mathf.Abs(box.transform.position.x - (R.GetZero().x+R.GetSize().x)) < 2.0f ||
                Mathf.Abs(box.transform.position.z - R.GetZero().z) < 2.0f ||
                Mathf.Abs(box.transform.position.z - (R.GetZero().z+R.GetSize().z)) < 2.0f)
            {
                Vector3 dir = (R.GetZero() + R.GetSize() / 2 - box.transform.position).normalized;
                box.GetComponent<Rigidbody>().MovePosition(box.transform.position + (dir * Time.deltaTime));
            }
        }

        if(R.solved && Vector3.Distance(box.transform.position,TargetTile.transform.position) > 0.1f)
        {
            Vector3 dir = (TargetTile.transform.position-box.transform.position).normalized/2.0f;
            box.GetComponent<Rigidbody>().MovePosition(box.transform.position+(dir*Time.deltaTime));
        }

	}
    public void Solve(bool s) { solved = s; }
    public bool isSolved() { return solved; }

}
