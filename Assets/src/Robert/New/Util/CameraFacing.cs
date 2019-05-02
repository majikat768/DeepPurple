

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
