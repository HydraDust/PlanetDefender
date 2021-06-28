using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTurn : MonoBehaviour
{
    public Transform body;

    Transform target;

    void Start()
    {
        target = FindObjectOfType<GameManager>().player.transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < 6f)
        {
            Vector3 inputDir = transform.InverseTransformPoint(target.position);
            inputDir.y = 0;
            inputDir = inputDir.normalized;
            
            Vector3 focus = body.position + (transform.right * inputDir.x) + (transform.forward * inputDir.z);
            Vector3 relativePos = focus - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos, body.up);
            body.rotation = Quaternion.Lerp(body.rotation, toRotation, 20f * Time.deltaTime);
        }
    }
}
