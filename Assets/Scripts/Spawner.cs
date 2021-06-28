using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject spawnVFX;

    public float timeBetweenWavesMin;
    public float timeBetweenWavesMax;
    float nextTimeToSpawn;
    public int waveSizeMin;
    public int waveSizeMax;
    public int maxEnemies;

    GameObject player;

    void Start()
    {
        player = GetComponent<GameManager>().player;
    }

    void Update()
    {
        if (Time.time >= nextTimeToSpawn && Enemy.allEnemies.Count < maxEnemies && player.GetComponent<Health>().IsAlive())
        {
            StartCoroutine(SpawnWave());
            nextTimeToSpawn = Time.time + Random.Range(timeBetweenWavesMin, timeBetweenWavesMax);
        }
    }

    IEnumerator SpawnWave()
    {        
        int enemy = Random.Range(0, enemies.Length);
        int waveSize = Random.Range(waveSizeMin, waveSizeMax + 1);
        for (int i = 0; i < waveSize; i++)
        {
            StartCoroutine(SpawnEnemy(enemy));
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpawnEnemy(int index)
    {
        Vector3 spawnPosition;
        bool clear = false;
        int tries = 0;
        do
        {
            spawnPosition = Random.onUnitSphere * 5.1f;

            Collider[] nearEnemies = Physics.OverlapSphere(transform.position, 0.4f, (1 << 8));
            if (nearEnemies.Length == 0)
            {
                clear = true;
            }

            tries += 1;
        } while (!clear && tries < 10);

        Quaternion spawnRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Random.insideUnitSphere, (spawnPosition - transform.position)).normalized, (spawnPosition - transform.position).normalized);

        GameObject spawnVFXGO = Instantiate(spawnVFX, spawnPosition, spawnRotation);
        spawnVFXGO.transform.Rotate(new Vector3(90, 0, 0));
        yield return new WaitForSeconds(3f);
        Destroy(spawnVFXGO);

        if (player.GetComponent<Health>().IsAlive())
            Instantiate(enemies[index], spawnPosition, spawnRotation);
    }

}
