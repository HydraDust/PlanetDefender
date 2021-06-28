using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    void OnTriggerEnter(Collider hit)
    {
        Projectile p = hit.GetComponent<Projectile>();
        if (p)
        {
            if (!p.enemyTarget)
            {
                hit.gameObject.SetActive(false);
            }
        }
    }
}
