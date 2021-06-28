using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed;

    Vector3 rotation;

    void Start()
    {
        rotation = new Vector3(0, speed, 0);
    }

    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
