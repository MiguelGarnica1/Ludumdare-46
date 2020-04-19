using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemy;
        public int count;
        public float rate;
    }

    public Transform[] spawnPoint;
    public SpawnState state = SpawnState.COUNTING;
    public Wave[] waves; 
    public static int numOfEnemy; //The total number of enemy   
    private int currentWave; //The current wave level

    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    void Start()
    {
        waveCountDown = timeBetweenWaves;    
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            //Check if enemy is still alive
            if (numOfEnemy == 0)
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        //When countdown reach 0, start spawning
        if (waveCountDown <= 0 && state != SpawnState.SPAWNING)
        {
            StartCoroutine(SpawnWave(waves[currentWave]));
             
        }
        else //Start the countdown
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;
        numOfEnemy = _wave.count;

        for (int i = 0; i < _wave.count; i++)
        {
            //Spawn Enemy randomly from the list of enemy
            SpawnEnemy(_wave.enemy[Random.Range(0, _wave.enemy.Length)]);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    /*
     * Spawn enemy in the current wave
     */
    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawnning TREESSS");
        Transform point = spawnPoint[Random.Range(0, spawnPoint.Length)];
        Instantiate(enemy, point.position, point.rotation);
    }

    /*
     * Is called when a wave is completed
     */
    void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (currentWave + 1 > waves.Length - 1)
        {
            currentWave = 0;
        }
        else
        {
            currentWave++;
        }
    }
}
