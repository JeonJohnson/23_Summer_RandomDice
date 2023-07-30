using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPoint;
    public PoolManager pool;
    SpriteRenderer spriter;

    public int count;
    public int curCount;
    public int maxCount;

    public bool isWavePlayed;

    public float delay = 1f;

    public static List<Enemy> aliveEnemies = new List<Enemy>();

    void Awake()
    {
        aliveEnemies.Clear();
    }

    public void Spawn()
    {
        if (!isWavePlayed) StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        count = 0;
        aliveEnemies.Clear();
        isWavePlayed = true;
        while (count < maxCount && isWavePlayed)
        {
            GameObject enemy = pool.Get(0);
                        
            enemy.transform.position = spawnPoint.position;
            enemy.GetComponent<Enemy>().Init();
            spriter = enemy.GetComponent<SpriteRenderer>();
            spriter.sortingOrder = maxCount - curCount +1;
            aliveEnemies.Add(enemy.GetComponent<Enemy>());
            yield return new WaitForSeconds(delay);
            curCount += 1;
            count += 1;
        }
    }
}