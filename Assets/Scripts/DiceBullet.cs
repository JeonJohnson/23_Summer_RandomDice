using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiceBullet : MonoBehaviour
{
    public float speed = 5;
    public Color color;
    Vector3 dir;

    void Start()
    {
        
    }

    void Update()
    {
        GameObject target = GameObject.Find("Enamy");
        dir = target.transform.position - transform.position;
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            Dice bullet = GameObject.Find("Dice").GetComponent<Dice>();
            bullet.bulletObjectPool.Add(collision.gameObject);
        }
    }
}
