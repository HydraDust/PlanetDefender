using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float positionSpeed;
    public float lookSpeed;

    void Update()
    {
        if (target == null)
            return;
        
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * positionSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * lookSpeed);
    }
}
