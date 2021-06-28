using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementChase : MonoBehaviour
{
    public Transform body;

    public float speed;

    Vector3 direction;

    Transform target;

    void Start()
    {
        target = FindObjectOfType<GameManager>().player.transform;
    }

    void Update()
    {
        direction = transform.InverseTransformPoint(target.position);
        direction.y = 0;
        direction = direction.normalized;
        
        if (direction.magnitude >= 0.1f)
        {
            transform.RotateAround(Vector3.zero, -transform.forward, speed * direction.x * Time.deltaTime);
            transform.RotateAround(Vector3.zero, transform.right, speed * direction.z * Time.deltaTime);

            Vector3 focus = body.position + (transform.right * direction.x) + (transform.forward * direction.z);
            Vector3 relativePos = focus - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos, body.up);
            body.rotation = Quaternion.Lerp(body.rotation, toRotation, 20f * Time.deltaTime);
        }
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

}
