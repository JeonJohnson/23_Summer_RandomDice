using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Transform spawnPoint;
    public PoolManager pool;
    SpriteRenderer spriter;
    public Text waveText;
    public Text timeText;

    [Header("Wave")]
    public bool isWavePlayed;
    public int wave;
    public int curTime;
    public int waveTime;
    public bool bossKill;

    [Header("Setting")]
    public int count;
    public int curCount;
    //public int maxCount;
    public float delay = 1f;

    public static List<Enemy> aliveEnemies = new List<Enemy>();

    void Awake()
    {
        aliveEnemies.Clear();
        bossKill = false;
        Timer();
    }

    public void Spawn()
    {
        if (!isWavePlayed) StartCoroutine(SpawnEnemy());
    }

    public void Timer()
    {
        if (!isWavePlayed) StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        wave += 1;
        waveText.text = wave.ToString("00");
        curTime = waveTime;
        timeText.text = (curTime / 60).ToString("00") + ":" + (curTime % 60).ToString("00");
        yield return new WaitForSeconds(3f);
        Spawn();
        while (true)
        {
            while (curTime > 0)
            {
                curTime -= 1;
                timeText.text = (curTime / 60).ToString("00") + ":" + (curTime % 60).ToString("00");
                yield return new WaitForSeconds(1f);
            }
            if (bossKill)
            {
                wave += 1;
                waveText.text = wave.ToString("00");
                curTime = waveTime;
                timeText.text = (curTime / 60).ToString("00") + ":" + (curTime % 60).ToString("00");
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }

    IEnumerator SpawnEnemy()
    {
        count = 0;
        isWavePlayed = true;
        while (true)
        {
            while (curTime >= 0 && isWavePlayed)
            {
                if (curTime > 0)
                {
                    GameObject enemy = pool.Get(Random.Range(0, 1));
                    enemy.transform.position = spawnPoint.position;
                    enemy.GetComponent<Enemy>().Init();
                    spriter = enemy.GetComponent<SpriteRenderer>();
                    spriter.sortingOrder = 1000 - curCount;
                    aliveEnemies.Add(enemy.GetComponent<Enemy>());
                    curCount += 1;
                    count += 1;
                    if (bossKill)
                    {
                        bossKill = false;
                    }
                }
                if (aliveEnemies.Count == 0 && curCount == 0 && curTime == 0 && !bossKill)
                {
                    GameObject enemy = pool.Get(Random.Range(2, 3));
                    enemy.transform.position = spawnPoint.position;
                    enemy.GetComponent<Enemy>().Init();
                    spriter = enemy.GetComponent<SpriteRenderer>();
                    spriter.sortingOrder = 1000 - curCount;
                    aliveEnemies.Add(enemy.GetComponent<Enemy>());
                }
                    yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}