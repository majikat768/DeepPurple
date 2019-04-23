/* EnemyStats.cs
 * Programmer: Robert Goes
 * Description: This servers as the subject in an observer pattern, where it gets reports on enemy locations, and stores it's
 * own refrence to the player gameobject, and reports back to the enemys the players stats. It is a Singleton also since it should be only be used once per level.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : SingletonEnemy<EnemyStats>
{

    public float awakeRange = 10;
    private HashSet<ICallback> callbacks = new HashSet<ICallback>();

    private GameObject player;

    private List<float> distances = new List<float>();

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    
    }
    public void FixedUpdate()
    {
        Vector3 playerPos = player.transform.position;
        float distPlayer;
        var en = callbacks.GetEnumerator();

        while (en.MoveNext())
        {
            GameObject Observer;
            en.Current.GetGameobject(out Observer);
            distPlayer = Vector3.Distance(Observer.transform.position, playerPos);
            if (distPlayer <= awakeRange)
            {
                en.Current.Invoke();
            }

        }
    }

    public void AddObserver(ICallback callback)
    {
        callbacks.Add(callback);
    }

    public void RemoveObserver(ICallback callback)
    {
        callbacks.Remove(callback);
    }

    public void SignalCallback()
    {
        var en = callbacks.GetEnumerator();
        while (en.MoveNext())
        {
            en.Current.Invoke();
        }
    }

}

