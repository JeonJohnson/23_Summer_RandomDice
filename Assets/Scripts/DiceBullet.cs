using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBullet : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SerializeDiceData serializeDiceData;
    [SerializeField] float speed;

    public DiceDate diceDate => GameManager.Inst.diceSo.GetDiceDate(serializeDiceData.code);

    Enemy targetEnemy;

    public void SetupDiceBullet(SerializeDiceData serializeDiceData, Enemy targetEnemy)
    {
        this.serializeDiceData = serializeDiceData;
        this.targetEnemy = targetEnemy;
        spriteRenderer.color = diceDate.color;

        StartCoroutine(AttackCo());
    }

    IEnumerator AttackCo()
    {
        while (true)
        {
            if (targetEnemy == null || targetEnemy.health <= 0)
            {
                Die();
                yield break;
            }

            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
            yield return null;

            if ((transform.position - targetEnemy.transform.position).sqrMagnitude < speed * Time.deltaTime * speed * Time.deltaTime)
            {
                transform.position = targetEnemy.transform.position;
                break;
            }
        }

        int totalAttackDamage = Utils.TotalAttackDamage(diceDate.basicAttackDamage, serializeDiceData.level);

        if (GameManager.Inst.enemies.Count == 0)
        {
            yield return null;
        }

        if (targetEnemy != null || targetEnemy.health - totalAttackDamage >= 0)
            targetEnemy.takeEnemyDamage(totalAttackDamage, targetEnemy);

        Die();
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
