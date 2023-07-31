using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;

    public bool isLive;
    public bool isBoss;

    Rigidbody2D rigid;
    Text text;
    Spawner spawner;

    public Transform[] wayArray;
    public int targetWay = 2;
    Vector2 target;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        wayArray = GameObject.Find("Way").GetComponentsInChildren<Transform>();
        text = GetComponentInChildren<Text>();
        spawner = GameObject.Find("EnemySpawner").GetComponent<Spawner>();
    }

    void FixedUpdate()
    {
        if (!isLive) return;

        DrawText();
        target = wayArray[targetWay].position;
        Vector2 dirVec = target - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        Vector3 myPos = transform.position;
        Vector3 targetPos = target;
        float curDiff = Vector3.Distance(myPos, targetPos);
        if (curDiff <= 0.1)
        {
            switch (targetWay)
            {
                case 2:
                    targetWay = 3;
                    break;
                case 3:
                    targetWay = 4;
                    break;
                case 4:
                    if (isBoss) Arrive(3);
                    else Arrive(1);
                    break;
            }
        }
    }

    void OnEnable()
    {
        isLive = true;
        rigid.simulated = true;
        targetWay = 2;
        DrawText();
    }

    public void Init()
    {
        targetWay = 2;
        DrawText();
    }

    void Dead()
    {
        gameObject.SetActive(false);

        if(isLive == true && !isBoss)
        {
            spawner.curCount -= 1;
            isLive = false;
        }
        if (isBoss && !spawner.bossKill) {
            spawner.bossKill = true;
            isLive = false;
            Debug.Log("Boss Kill");
        }
        Spawner.aliveEnemies.Remove(this);
    }

    void Arrive(int value)
    {
        gameObject.SetActive(false);
        if (!isBoss) {
            spawner.curCount -= 1;
        }
        Spawner.aliveEnemies.Remove(this);
        Player.instance.takePlayerDamage(value);
    }

    void DrawText()
    {
        text.text = health.ToString();
    }

    public void takeEnemyDamage(int dmg, Enemy target)
    {
        if (target.health - dmg > 0)
        {
            target.health -= dmg;
        }
        else
        {
            target.health = 0;
            Dead();
        }
    }
}
