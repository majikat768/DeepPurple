/* ICallback.cs
 * Programmer: Robert Goes
 * Description: Serves as a interface for performing a callback for the observer, enemystats
 */

using UnityEngine;

public interface ICallback
{
    //function that is required on a class that uses this interface
     void UpdatePos(Vector3 PlayerPos);
}
