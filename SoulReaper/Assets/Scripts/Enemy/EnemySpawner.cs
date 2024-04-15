using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnLocs;
    public int[] numOfEnemiesPerWave;
    public GameObject enemy;
    public float currentNumOfEnemies;
    //public List<GameObject> currentEnemies;

    private int waveIndex;

    // Start is called before the first frame update
    void Start()
    {
        int num = numOfEnemiesPerWave[0];
        waveIndex = 1;

        for (int i = 0; i < num; i++)
        {
            Debug.Log(i);
            int l = Random.Range(0, spawnLocs.Length);
            GameObject obj = Instantiate(enemy, spawnLocs[l]);
            Debug.Log(obj.name + " SPAWNING");
        }

        currentNumOfEnemies = num;

        StartCoroutine(CheckWave());
    }

    private void Update()
    {
        //Debug.Log(currentNumOfEnemies);
    }

    IEnumerator CheckWave()
    {
        yield return new WaitUntil(() => currentNumOfEnemies == 0);

        int l = Random.Range(0, spawnLocs.Length);
        for (int i = 0; i < numOfEnemiesPerWave[waveIndex]; i++)
        {
            GameObject obj = Instantiate(enemy, spawnLocs[l]);
        }
        if (waveIndex < numOfEnemiesPerWave.Length) 
        {
            waveIndex++;
            currentNumOfEnemies = numOfEnemiesPerWave[waveIndex];
            StartCoroutine(CheckWave());
        }
    }
}
