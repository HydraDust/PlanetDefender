using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpin : MonoBehaviour
{
    public float speed;

    Vector3 rotation;

    void Start()
    {
        rotation = new Vector3(0, 1, 0) * speed;
    }

    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
