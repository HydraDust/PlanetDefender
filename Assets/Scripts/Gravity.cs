using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityForce = 10;
 
    void FixedUpdate()
    {
        Rigidbody[] rigBods = FindObjectsOfType<Rigidbody>();
        foreach (var i in rigBods)
        {
            if (!i.isKinematic)
            {
                i.useGravity = false;
                i.AddForce((transform.position - i.position).normalized * gravityForce, ForceMode.Acceleration);
            }
        }
    }
}
