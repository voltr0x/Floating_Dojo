using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private PlayerController playerControllerScript;

    public int enemyCount;
    private int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Get gameover bool status
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;

        if(enemyCount == 0 && !playerControllerScript.gameOver)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnX = Random.Range(-9, 9);
        float spawnZ = Random.Range(-9, 9);

        Vector3 randomPos = new Vector3(spawnX, 0, spawnZ);

        return randomPos;
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i=0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        Instantiate(powerupPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
    }
}
