/* Registry.cs
 * Programmer: Zach Sugano
 * Description: This script acts as a central database for any information that needs to
 * be stored across scenes. It uses a dictionary which maps strings to arbitrary objects.
 * This is an example of dynamic binding as the actual type of the object can be anything
 * and the compiler does not know ahead of time what objects are going to be stored there.
 * 
 * This gives us great flexibility for storing things like settings, score, and other
 * information without having to create separate variables for each piece of information
 * that we want to store. This also prevents us from having to pass information around between
 * classes and scripts since this can all be statically accessed.
 * 
 */

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
