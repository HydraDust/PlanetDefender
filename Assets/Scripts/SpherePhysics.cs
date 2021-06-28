using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePhysics : MonoBehaviour
{
    public float mass;
    public float drag;

    Vector3 force = Vector3.zero;

    public void AddForce(Vector3 direction, float strength)
    {
        force += direction.normalized * strength / mass;
    }

    public void SetForce(Vector3 newForce)
    {
        force = newForce;
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, -transform.forward, force.x * Time.deltaTime);
        transform.RotateAround(Vector3.zero, transform.right, force.z * Time.deltaTime);
        
        force.x = Mathf.Lerp(force.x, 0, drag * Time.deltaTime);
        force.z = Mathf.Lerp(force.z, 0, drag * Time.deltaTime);
    }
}
