/* BaseEnemy.cs
 * Programmer: RobertGoes
 * This is the base class for all diffrent types of enemies.
 * There will be enemy subclasses that inhert from this and overide behavior.
 * 
 */

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour
{
    //protected variables, initialized by start
    protected GameObject rifle_1; //rifle the enemy has
    protected NavMeshAgent agent; //the navmesh agent of the enemy
    protected EnemyStats stats;   //an observer link to the target stats.
    protected Actions action;     //set of actions for animator controller
    protected GameObject player; //stores refrence to player gameobject
    protected Animator animator; //animator for avatar

    //updated by fixed updated
    protected Vector3 playerPos; //stores player position
    protected float playerDist; //stores player distance

    [SerializeField]
    public Transform rightGunBone;
    [SerializeField]
    public GameObject weapon;
    [SerializeField]
    public GameObject bullet;
    [SerializeField]
    public int health;
    [SerializeField]
    public float attackDist; //distance from which the ai will begin shooting



    /// <summary>
    /// State machine to control behavior. Each enum in State must match a function of the
    /// same name. All possible states that a subclass may overide must be present here.
    /// </summary>
    //defines state the enemy is in
    protected enum State
    {
        WANDER,
        CHASE,
        SNEAK,
        ATTACK,
        FLEE,
        REST,

    }

    //defines current state
    protected State state;
    protected State lastState;
    //non-unity

    /// <summary>
    /// Executes the current state of the enemy. Requires the function name
    /// for each state to be the same as the state in the enum
    /// </summary>
    /// <returns>Returns IEnumerator in order to act as a coroutine</returns>
    protected IEnumerator FSM()
    {
        Debug.Log("FSM Started:"+ state.ToString());
        // Execute the current coroutine (state)
        while (true)
        {
            yield return StartCoroutine(state.ToString());
        }
    
    }

    //--------------------begin state machine-------------------
    //overidable state
    protected virtual IEnumerator WANDER()
    {
        //Enter state
        Debug.Log("Entering:" +state);
        yield return null;

        //Execute state
        Debug.Log("Inside:"+state);
        for (var i = 0; i < 10; i++)
        {
            Debug.Log("Executing:"+state+" Time:"+ Time.time);
            yield return new WaitForSeconds(.1f);
        }

        //Leave state
        
        lastState = state;
        state = State.REST;  //if no player has been found for a while, take a rest
        Debug.Log("Leaving:" + lastState + "Entering:" + state);
    }

    //overidable state
    protected virtual IEnumerator CHASE()
    {
        //Enter state
        Debug.Log("Entering:" + state);
        yield return null;

        //Execute state
        Debug.Log("Inside:" + state);
        for (var i = 0; i < 10; i++)
        {
            Debug.Log("Executing:" + state + " Time:" + Time.time);
            yield return new WaitForSeconds(.1f);
        }

        //Leave state

        lastState = state;
        state = State.WANDER; // if player got away, go back to wandering
        Debug.Log("Leaving:" + lastState + "Entering:" + state);
    }

    //overidable state
    protected virtual IEnumerator SNEAK()
    {
        //Enter state
        Debug.Log("Entering:" + state);
        yield return null;

        //Execute state
        Debug.Log("Inside:" + state);
        for (var i = 0; i < 10; i++)
        {
            Debug.Log("Executing:" + state + " Time:" + Time.time);
            yield return new WaitForSeconds(.1f);
        }

        //Leave state

        lastState = state;
        state = State.WANDER; //sneak atttack failed, go back to wandering
        Debug.Log("Leaving:" + lastState + "Entering:" + state);
    }

    //Attack state
    protected virtual IEnumerator ATTACK()
    {
        //Enter state
        Debug.Log("Entering:" + state);
        yield return new WaitForFixedUpdate();  //we are doing stuff at physics level now
        
        //Execute state
        Debug.Log("Inside:" + state);

        //Aim at player

        //set  animation
        animator.SetBool("Squat", false);
        animator.SetFloat("Speed", 0f);
        animator.SetBool("Aiming", true);


        //when fire
        animator.SetTrigger("Attack");
        for (var i = 0; i < 10; i++)
        {
            Debug.Log("Executing:" + state + " Time:" + Time.time);
            yield return new WaitForSeconds(.1f);
        }

        //Leave state

        lastState = state;
        state = State.FLEE; //attack failed, flee
        Debug.Log("Leaving:" + lastState + "Entering:" + state);
    }

    //overidable state
    protected virtual IEnumerator FLEE()
    {
        //Enter state
        Debug.Log("Entering:" + state);
        yield return null;

        //Execute state
        Debug.Log("Inside:" + state);
        for (var i = 0; i < 10; i++)
        {
            Debug.Log("Executing:" + state + " Time:" + Time.time);
            yield return new WaitForSeconds(.1f);
        }

        //Leave state

        lastState = state;
        state = State.REST; //flee to rest area
        Debug.Log("Leaving:" + lastState + "Entering:" + state);
    }

    //overidable state
    protected virtual IEnumerator REST()
    {
        //Enter state
        Debug.Log("Entering:" + state);
        yield return null;

        //Execute state
        Debug.Log("Inside:" + state);

        //Set animator for standing
        animator.SetBool("Aiming", false);
        animator.SetFloat("Speed", 0f);

      
        for (var i = 0; i < 10; i++)
        {
            Debug.Log("Executing:" + state + " Time:" + Time.time);
            yield return new WaitForSeconds(.1f);
        }

        //Leave state

        lastState = state;
        state = State.WANDER; //go back to wandering after resting
        Debug.Log("Leaving:" + lastState + "Entering:" + state);
    }



    //----------------------------end state machine---------------------------

    //Attachs weapon to base avatar, no overide allowed since it is specfic
    protected void attachWeapon()
    {
        GameObject rifle_1 = (GameObject)Instantiate(weapon);
        rifle_1.transform.parent = rightGunBone;
        rifle_1.transform.localPosition = Vector3.zero;
        rifle_1.transform.localRotation = Quaternion.Euler(90, 0, 0);
    }

    //start method for sub class
    protected virtual void subStart()
    {
        attackDist = 10f;
        health = 100;
        Debug.Log("SubEnemy has not implemented subStart, health default 100");
    }
    
    //sets the start state for the FSM
    protected virtual void setStartState()
    {
        state = State.WANDER;
        Debug.Log("SubEnemy has not implemented setStartState, defaulting to:"+state);
    }

    //unity functions

    //Called on when the object is awoken
    protected virtual void Awake()
    {
        
    }

    // Use this for initialization, called once for a given script
    protected void Start ()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        action = this.gameObject.GetComponent<Actions>();

        player = GameObject.FindWithTag("Player");
        Debug.Assert(player != null, "No player was found in scene");

        playerPos = player.transform.position;  //store the players position
        playerDist = Vector3.Distance(transform.position, playerPos); //stores distance to player
        attachWeapon();

        StartCoroutine(FSM());

        subStart();
    }

    //Called when physics updates happen, may happen more then once per frame
    protected virtual void FixedUpdate()
    {
        playerPos = player.transform.position; //update Player position
        playerDist = Vector3.Distance(transform.position, playerPos);
    }

    //called on entering trigger
    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }

    //called on exiting trigger
    protected virtual void OnTriggerExit(Collider other)
    {
        
    }

    //called on entering trigger
    protected virtual void OnCollisionEnter(Collision collision)
    {
        
    }

    //called on exiting trigger
    protected virtual void OnCollisionExit(Collision collision)
    {

    }

    // Update is called once per frame
    protected virtual void Update ()
    {
    }

    //Called after update has finished
    protected virtual void LateUpdate()
    {

    }

    //helper functions 
    private Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    //returns wheather the player is range of the max detection setting
    protected bool playerInView(float FOV)
    {
        bool inSight = false; //stores if player in sight

        Vector3 direction = (player.transform.position - transform.position).normalized;

        float angle = Vector3.Angle(transform.forward,direction);
        if(angle < FOV/2f)
        {
            float dstToTarget = playerDist;
            if (!Physics.Raycast(transform.position,direction,dstToTarget,-1))
            {
                inSight = true;
            }
        }
        return inSight;
    }
    protected void drawFOV(float FOV)
    {
        float totalFOV = FOV;
        float rayRange = playerDist;
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Debug.DrawRay(transform.position, leftRayDirection * rayRange, Color.red);
        Debug.DrawRay(transform.position, rightRayDirection * rayRange, Color.red);
        // Debug.DrawRay(transform.position, direction, Color.green);
    }
}
