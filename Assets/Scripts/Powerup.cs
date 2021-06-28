using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Powerup : MonoBehaviour
{
    [SerializeField] public UnityEvent OnCollect = new UnityEvent();

    public int type;

    public GameObject collectVFX;

    Transform player;

    void Start()
    {
        player = FindObjectOfType<GameManager>().player.transform;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = FindObjectOfType<GameManager>().player;

            switch (type)
            {
                case 0:
                    player.GetComponent<PowerupRapidFire>().Activate();
                    break;
                case 1:
                    player.GetComponent<PowerupMoreChannels>().Activate();
                    break;
                default:
                    break;
            }

            if (collectVFX)
                Instantiate(collectVFX, transform.position, transform.rotation);

            OnCollect.Invoke();

            AudioManager.instance.Play("CollectPowerup");

            Destroy(gameObject);
        }
        
    }

    void Update()
    {
        if (player)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance < 7f)
            {
                Vector3 direction = transform.InverseTransformPoint(player.position);
                direction.y = 0;
                direction = direction.normalized;
                
                if (direction.magnitude >= 0.1f)
                {
                    transform.RotateAround(Vector3.zero, -transform.forward, (20f / distance) * direction.x * Time.deltaTime);
                    transform.RotateAround(Vector3.zero, transform.right, (20f / distance) * direction.z * Time.deltaTime);
                }
            }
        }
    }
}
