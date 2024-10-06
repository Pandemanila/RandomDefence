using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private EnemySpawner spawner;
    [SerializeField]
    private PlayerStat playerStat;
    [SerializeField]
    private GameManager gameManagerOver;
    private int currentWaveIndex = -1;
    public float time = 10f;
    public string timeText;
    [SerializeField]
    private GameManager gameManagerVic;



    public int CurrentWave => currentWaveIndex + 1;

    private void Update()
    {
        if (spawner.EnemyList.Count < 81)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                int seconds = Mathf.FloorToInt(time % 60);
                int minutes = Mathf.FloorToInt(time / 60);
                timeText = string.Format("{0:00} :{1:00}", minutes, seconds);
            }
            else
            {
                StartWave();
                time = 55f;
            }
        }
        else if (spawner.EnemyList.Count >= 81)
        {
            StartCoroutine("DestroyAllEnemy");
            if(spawner.EnemyList.Count == 0)
            {
                StopCoroutine("DestroyAllEnemy");
            }
            gameManagerOver.OnGamePanel();

        }

        
    }
    public void StartWave()
    { 
        if(currentWaveIndex < waves.Length-1)
        {
            currentWaveIndex++;
            spawner.StartWave(waves[currentWaveIndex]);
        }
        else if(currentWaveIndex >= waves.Length-1)
        {
            gameManagerVic.OnGamePanel();
        }
        if(currentWaveIndex >=1)
        {
            playerStat.FinshWave();
        }
        
    }

    IEnumerator DestroyAllEnemy()
    {
        yield return null;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}

