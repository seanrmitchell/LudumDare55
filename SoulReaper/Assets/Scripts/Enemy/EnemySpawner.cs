using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public Transform[] spawnLocs;
    public int numOfEnemiesPerWave;
    public float waveEnemyIncrease;
    public GameObject enemy;
    public float currentNumOfEnemies;
    //public List<GameObject> currentEnemies;

    private int waveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Spawner();
    }

    private void Spawner()
    {
        numOfEnemiesPerWave = (int)(numOfEnemiesPerWave + Mathf.Round(numOfEnemiesPerWave * waveEnemyIncrease));

        for (int i = 0; i < numOfEnemiesPerWave; i++)
        {
            int l = Random.Range(0, spawnLocs.Length);
            Vector3 add = new Vector2(spawnLocs[l].position.x + Random.Range(-2, 2), spawnLocs[l].position.y + Random.Range(-2, 2));
            Vector3 temp = spawnLocs[l].position;
            spawnLocs[l].position = add;
            GameObject obj = Instantiate(enemy, spawnLocs[l]);
            spawnLocs[l].position = temp;
        }

        currentNumOfEnemies = numOfEnemiesPerWave;
        waveIndex++;
        waveText.text = waveIndex.ToString();
        StartCoroutine(CheckWave());
    }

    IEnumerator CheckWave()
    {
        yield return new WaitUntil(() => currentNumOfEnemies <= 0);
        yield return new WaitForSeconds(1f);
        Spawner();
    }
}
