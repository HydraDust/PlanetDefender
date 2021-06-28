using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerups;
    GameObject currentPowerup;

    public float timeBetweenSpawns;
    float nextTimeToSpawn;
    bool readyToSpawn = true;

    GameObject player;

    void Start()
    {
        player = GetComponent<GameManager>().player;
        nextTimeToSpawn = Time.time + timeBetweenSpawns;
    }

    void Update()
    {
        if (readyToSpawn && Time.time >= nextTimeToSpawn && player.GetComponent<Health>().IsAlive())
        {
            SpawnPowerup();
        }
    }

    void SpawnPowerup()
    {        
        Vector3 spawnPosition = Random.onUnitSphere * 5.1f;
        Quaternion spawnRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Random.insideUnitSphere, (spawnPosition - transform.position)).normalized, (spawnPosition - transform.position).normalized);
        currentPowerup = Instantiate(powerups[Random.Range(0, powerups.Length)], spawnPosition, spawnRotation);
        currentPowerup.GetComponent<Powerup>().OnCollect.AddListener(PowerupCollected);
        readyToSpawn = false;
    }

    void PowerupCollected()
    {
        readyToSpawn = true;
        nextTimeToSpawn = Time.time + timeBetweenSpawns;
    }

}
