/* EnemyStats.cs
 * Programmer: Robert Goes
 * Description: This servers as the subject in an observer pattern, where it gets reports on enemy locations, and stores it's
 * own refrence to the player gameobject, and reports back to the enemys the players stats. 
 * It is a Singleton also since it should be only be used once per level.
 * It is also an example of a many to one has a relationship, since it has many observers that it "pushes info to.
 * Also, since the type of the obsever is not known and compile time, and the method for update pos is overidable,
 * it is an examplle of dynamic binding being used
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : SingletonEnemy<EnemyStats>
{
    private HashSet<ICallback> callbacks = new HashSet<ICallback>();
    public Vector3 newPlayerPos = new Vector3(0,0,0);
    private GameObject player;
    public void Start()
    {
        player = GameObject.Find("vThirdPersonPlayer");
    }
    public void FixedUpdate()
    {
       player = GameObject.Find("vThirdPersonPlayer");
        Vector3 oldPlayerPos = player.transform.position;
       newPlayerPos = player.transform.position;
        Debug.Log("EnemyStats::updatedPlayerPos" + newPlayerPos);
        if (oldPlayerPos != newPlayerPos)
        {
            SignalCallback(newPlayerPos);
            Debug.Log("EnemyStats::updatedPlayerPos" + newPlayerPos);
        }

    }

    public void AddObserver(ICallback callback)
    {
        //adds an observer to callback to with a value once it has changed
        callbacks.Add(callback);
    }

    public void RemoveObserver(ICallback callback)
    {
        //removes an observer from it's list of objects observering it
        callbacks.Remove(callback);
    }

    public void SignalCallback(Vector3 PlayerPos)
    {   
        var en = callbacks.GetEnumerator();
        while (en.MoveNext())
        {
            //updates all enemies with player pos
            en.Current.UpdatePos(PlayerPos);
        } 
    }

}

