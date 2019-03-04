using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;

    public int moveMax = 16;
    public int moveMin = 2;

    void Update()
    {
        float distPlayer = Vector3.Distance(transform.position,player.transform.position);

        if(distPlayer <= moveMax && distPlayer >= moveMin)
        {
            agent.SetDestination(player.transform.position);
        }
        else
            agent.ResetPath();
    }
}

