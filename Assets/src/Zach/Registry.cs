using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The registry is a simple persistent key value storage that works between scenes.
 * You can store any key object pair to store arbitrary data as needed
 * 
 * Note: This does not store data between game launches. This is only between in-game scenes.
 * Note: For frequently accessed global storage, create a variable in here instead.
 **/
public class Registry : MonoBehaviour
{
    public static Dictionary<string, object> registry = new Dictionary<string, object>();

    public static TValue GetOrDefault<TValue>(string key, TValue defaultValue)
    {
        object value;
        return registry.TryGetValue(key, out value) ? (TValue)value : defaultValue;
    }

}
