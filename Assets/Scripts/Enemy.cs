using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float speed;
    public float health;
    public float maxHealth;

    bool isLive;

    Rigidbody2D rigid;
    Text text;
    Spawner spawner;

    public Transform[] wayArray;
    public int targetWay = 2;
    Vector2 target;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        wayArray = GameObject.Find("Way").GetComponentsInChildren<Transform>();
        text = GetComponentInChildren<Text>();
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
    }

    void FixedUpdate() {
        if (!isLive) return;

        DrawText();
        target = wayArray[targetWay].position;
        Vector2 dirVec = target - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        Vector3 myPos = transform.position;
        Vector3 targetPos = target;
        float curDiff = Vector3.Distance(myPos, targetPos);
        if (curDiff <= 0.1) {
            switch (targetWay) {
                case 2:
                    targetWay = 3;
                    break;
                case 3:
                    targetWay = 4;
                    break;
                case 4:
                    Dead();
                    break;
            }
        }
    }

    void OnEnable() {
        isLive = true;
        rigid.simulated = true;
        health = maxHealth;
        targetWay = 2;
        DrawText();
    }

    public void Init() {
        health = maxHealth;
        targetWay = 2;
        DrawText();
    }

    void Dead() {
        gameObject.SetActive(false);
        spawner.curCount -= 1;
        if (spawner.curCount == 0) StopCoroutine("SpawnEnemy");
    }

    void DrawText() {
        text.text = health.ToString();
    }
}
