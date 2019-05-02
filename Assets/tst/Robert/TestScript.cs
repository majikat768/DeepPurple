using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestScript : MonoBehaviour {
    public float wanderRadius;
    public float wanderTimer;
    public float FOV = 90;
    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    private GameObject player;
    // Use this for initialization
    void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }
    // Use this for initialization

    void Start () {
        
        //agent = GetComponent<NavMeshAgent>();
        /*
        Vector3 random_pos = Random.onUnitSphere * 10;
        random_pos.z = 0;

        Debug.Log(random_pos);*/
    }

    // Update is called once per frame
    void Update()
    {
        drawFOV(FOV, 10);
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
        bool in_view = playerInView(FOV);
        //Debug.Log("Player in view:" + in_view);
    }
    void FixedUpdate()
    {
        playerInView(FOV);
        drawFOV(FOV, 10);
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void drawFOV(float FOV, float length)
    {
        float totalFOV = FOV;
        float rayRange = Vector3.Distance(transform.position, player.transform.position);
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Debug.DrawRay(transform.position, leftRayDirection * rayRange, Color.red);
        Debug.DrawRay(transform.position, rightRayDirection * rayRange, Color.red);
    }

    protected bool playerInView(float FOV)
    {
        bool inSight = false; //stores if player in sight
        //Debug.Log("dstToTarget:" + Vector3.Distance(transform.position, player.transform.position));
        Vector3 direction = (player.transform.position - transform.position).normalized;

        float angle = Vector3.Angle(transform.forward, direction);
        if (angle < FOV / 2f)
        {
            float dstToTarget = Vector3.Distance(transform.position, player.transform.position);

            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, dstToTarget);
            if(hit.collider.CompareTag("Player"))
            {
                Debug.DrawLine(ray.origin, hit.point);
                Debug.Log("Saw object at distance:"+dstToTarget);
                inSight = true;
            }
        }
        return inSight;
    }
}



/*
YieldInstruction wait = new WaitForSeconds(0.2f); // more memory-efficient to reuse the wait
             while(true)
             {
                 if (updatePath)
                     navAgent.SetDestination(target.position);
yield return wait;
             }

    */

