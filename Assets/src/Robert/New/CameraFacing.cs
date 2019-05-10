/* CameraFacing.cs
 * Programmer: RobertGoes
 * This is a script that I specficaly made and did not copy directly from a book that
 * said not to copy the code from since it was copywrited but could be argued as fair use
 * because of the nature of work by not meeting the threshold to be copywritable
 * that makes an object face the main camera.
 */

using UnityEngine;
using System.Collections;

public class CameraFacing : MonoBehaviour
{
    private Transform ThisTransform = null;
    void Awake()
    {
        ThisTransform = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        ThisTransform.LookAt(Camera.main.transform);
    }
}
