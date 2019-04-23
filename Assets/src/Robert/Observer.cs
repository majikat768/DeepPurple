using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour, ICallback
{
    public void GetGameobject(out GameObject Observer)
    {
        throw new System.NotImplementedException();
    }

    void ICallback.Invoke()
    {
        //do stuff
    }

}
