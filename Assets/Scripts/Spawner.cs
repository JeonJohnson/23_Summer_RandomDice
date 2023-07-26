using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform spawnPoint;
    public PoolManager pool;
    SpriteRenderer spriter;

    public int count;
    public int curCount;
    public int maxCount;

    public float delay = 1f;

    public void Spawn() {
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy() {
        count = 0;
        while (count < maxCount) {
            GameObject enemy = pool.Get(0);
            enemy.transform.position = spawnPoint.position;
            enemy.GetComponent<Enemy>().Init();
            spriter = enemy.GetComponent<SpriteRenderer>();
            spriter.sortingOrder = maxCount - curCount +1;
            yield return new WaitForSeconds(delay);
            curCount += 1;
            count += 1;
        }
    }
}