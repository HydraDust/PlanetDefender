using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject splitInto;
    public int splitAmount;

    Vector3 rotate;
    Transform body;

    void Start()
    {
        GetComponent<Health>().OnDeath.AddListener(Split);
        rotate = Random.onUnitSphere * 20f;
        body = transform.GetChild(0);
    }

    void Update()
    {
        body.Rotate(rotate * Time.deltaTime);


    }

    void Split()
    {
        if (splitInto)
        {
            for (int i = 0; i < splitAmount; i++)
            {
                Instantiate(splitInto, transform.position, transform.rotation);
            }
        }
        Destroy(gameObject);
    }
}
