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

    public bool isWavePlayed;

    public float delay = 1f;

    //근희 테스트
    public List<Enemy> aliveEnemies = new List<Enemy>();
    //근희 테스트

    public void Spawn() {
        if (!isWavePlayed) StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy() {
        count = 0;
        isWavePlayed = true;
        while (count < maxCount && isWavePlayed) {
            GameObject enemy = pool.Get(0);
            
            
            enemy.transform.position = spawnPoint.position;
            enemy.GetComponent<Enemy>().Init();
            spriter = enemy.GetComponent<SpriteRenderer>();
            spriter.sortingOrder = maxCount - curCount +1;

            //근희 테스트
            aliveEnemies.Add(enemy.GetComponent<Enemy>());
            //근희 테스트
            //적들은, 뒤지거나 혹은 골인지점에 도착하면 aliveEnemis에 접근해서 본인 지우기.


            yield return new WaitForSeconds(delay);
            curCount += 1;
            count += 1;
        }
    }
}