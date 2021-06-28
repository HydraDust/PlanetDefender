using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform body;

    public float speed;

    float switchTime;
    float nextTimeToChangeDirection;
    float nextTimeToCheckIsolation;

    Vector3 direction;

    void Start()
    {
        switchTime = 5f + Random.Range(-1.0f, 1.0f);
    }

    void Update()
    {
        if (Time.time >= nextTimeToChangeDirection)
        {
            switchTime = 5f + Random.Range(-1.0f, 1.0f);
            nextTimeToChangeDirection = Time.time + switchTime;

            Vector2 newRandomDirection = Random.insideUnitCircle;
            direction = new Vector3(newRandomDirection.x, 0, newRandomDirection.y).normalized;
        }

        if (Time.time >= nextTimeToCheckIsolation)
        {
            Collider[] nearEnemies = Physics.OverlapSphere(transform.position, 0.5f, (1 << 8));
            if (nearEnemies.Length > 1)
            {
                switchTime = 5f + Random.Range(-1.0f, 1.0f);
                nextTimeToChangeDirection = Time.time + switchTime;

                nextTimeToCheckIsolation = Time.time + 2f;

                direction = new Vector3(-direction.x, 0, -direction.y).normalized;
            }
        }


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

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

}
