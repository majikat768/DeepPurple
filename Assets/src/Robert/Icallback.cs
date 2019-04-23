/* ICallback.cs
 * Programmer: Robert Goes
 * Description: Serves as a interface for performing a callback for the observer, enemystats
 */

using UnityEngine;

public interface ICallback
{
    void GetGameobject(out GameObject Observer);
    void Invoke();

}
