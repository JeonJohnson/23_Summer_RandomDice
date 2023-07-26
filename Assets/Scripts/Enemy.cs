using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public float health;
    public float maxHealth;

    bool isLive;

    Rigidbody2D rigid;

    public Transform[] wayArray;
    public int targetWay = 2;
    Vector2 target;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        wayArray = GameObject.Find("Way").GetComponentsInChildren<Transform>();
    }

    void FixedUpdate() {
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
    }

    public void Init() {
        health = maxHealth;
        targetWay = 2;
    }

    void Dead() {
        gameObject.SetActive(false);
    }
}
