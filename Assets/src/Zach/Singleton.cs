/* Singleton.cs
 * Programmer: Zach Sugano
 * Description: Code was reused from http://wiki.unity3d.com/index.php/Singleton
 * This script allows us to create a singleton from any monobehavior simply by
 * making it extend this class.
 * 
 * There are no resulting copyright implications from utilizing this work.
 * In the court case Feist Publications, Inc., v. Rural Telephone Service Co., the
 * Supreme Court opinion stated that copyright protection could only be granted to
 * "works of authorship" that possess "at least some minimal degree of creativity".
 * Copyright protection extends only to an author's original, creative contribution
 * to a work. In this particular instance, the Singleton model is a well established
 * pattern in the field of computer science with widespread usage in countless programs.
 * This particular pattern is nearly identical in every way to any other Singleton
 * pattern implementation. Due to this code being mostly copied and lacking creative
 * contribution, we can safely say that this is not protected under copyright.
 * 
 */

using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object m_Lock = new object();
    private static T m_Instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (T)FindObjectOfType(typeof(T));

                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return m_Instance;
            }
        }
    }
}
