using UnityEngine;
using System.Collections.Generic;

public class PuzzleWavy : PuzzleRoom {
    
    GameObject ball;
    GameObject terrainObj;
    TerrainData td;
    float[,] heightArray;
    float scale;
    Texture2D txt;
    float t;
    float dropHeight;
    float waveSpeed;

    public override void Awake() {
        base.Awake();
        waveSpeed = 0.5f;
        t = 0.0f;
        complexity = -1;
        scale = 5;
        txt = Resources.Load<Texture2D>("Michael/Materials/FloorTiles");
    }

    void CreateTerrain() {
        TerrainData td = terrainObj.GetComponent<Terrain>().terrainData;
        float min = 1;
        float max = 0;
        for(int i = 0; i < size.x; i++) {
            for(int j = 0; j < size.z; j++) {
                float x = (float)i / size.x * scale + t;
                float y = (float)j / size.z * scale + t;
                float n = Mathf.PerlinNoise(x,y);
                if(n < min) min = n;
                if(n > max) max = n;
                heightArray[i,j] = n;

            }
        }
        float range = max - min;
        for(int i = 0; i < size.x; i++) {
            for(int j = 0; j < size.z; j++) {
                float n = heightArray[i,j];
                heightArray[i,j] = (n - min)/range;
            }
        }
        td.SetHeights(0,0,heightArray);
        t += waveSpeed*Time.deltaTime;

    }

    void Start() {
        td = new TerrainData();
        dropHeight = size.y*1;
        td.size = new Vector3(size.x,dropHeight,size.z) ;
        terrainObj = Terrain.CreateTerrainGameObject(td);
        terrainObj.transform.parent = this.transform;
        terrainObj.transform.position = Zero - new Vector3(0,dropHeight,0);

        Destroy(this.transform.Find("Floor").gameObject);
        heightArray = new float[(int)size.x,(int)size.z];
        //Destroy(this.transform.Find("Ceiling").gameObject);

        //td.size = size;
        CreateTerrain();

        List<SplatPrototype> splats = new List<SplatPrototype>();
        splats.Add(new SplatPrototype());
        splats[0].texture = txt;
        splats[0].tileOffset = new Vector2(0,0);
        splats[0].tileSize = new Vector2(4,4);
        td.splatPrototypes = splats.ToArray();


        BuildWall(Zero + new Vector3(0,-dropHeight,0),Zero + new Vector3(size.x,-dropHeight,0),dropHeight,false);
        BuildWall(Zero + new Vector3(0,-dropHeight,0),Zero + new Vector3(0,-dropHeight,size.z),dropHeight,false);
        BuildWall(Zero + new Vector3(size.x,-dropHeight,0),Zero + new Vector3(size.x,-dropHeight,size.z),dropHeight,false);
        BuildWall(Zero + new Vector3(0,-dropHeight,size.z),Zero + new Vector3(size.x,-dropHeight,size.z),dropHeight,false);

        for(int i = 0; i < 5; i++) {
            ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ball.transform.position=Zero+size/2;
            ball.AddComponent<Rigidbody>();
            ball.transform.parent = this.transform;
            
        }
            this.gameObject.GetComponent<BoxCollider>().center -= new Vector3(0,dropHeight,0);
            this.gameObject.GetComponent<BoxCollider>().size += new Vector3(0,dropHeight,0);

    }


    void FixedUpdate() {
        CreateTerrain();
    }

}
