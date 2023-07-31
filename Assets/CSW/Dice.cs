using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UIElements;

public class Dice : MonoBehaviour
{
    [Header("Conponents")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Order order;

    [Header("Values")]
    public SerializeDiceData serializeDiceData;
    [SerializeField] UnityEngine.Transform[] dots;

    public DiceDate diceDate => GameManager.Inst.diceSo.GetDiceDate(serializeDiceData.code);

    //근희 테스트 
    public int curIndex;
    //근희 테스트 

    public void SetupSlot(SerializeDiceData serializeDiceData)
    {
        this.serializeDiceData = serializeDiceData;
        var diceData = GameManager.Inst.diceSo.GetDiceDate(serializeDiceData.code);
        spriteRenderer.sprite = diceData.sprite;
        SetDots(serializeDiceData.level);

        for(int i = 0; i < Utils.MAX_DICE_LEVEL; i++)
        {
            dots[i].GetComponent<SpriteRenderer>().color = diceData.color;
        }
        StartCoroutine(DiceBulletSpawnCo());
    }

    public void SetDots(int level)
    {
        for (int i = 0; i < Utils.MAX_DICE_LEVEL; i++)
        {
            dots[i].gameObject.SetActive(i < level);
        }

        //위치
        Vector2[] positions = new Vector2[1];
        switch (level)
        {
            case 1: positions = new Vector2[] { Vector2.zero }; break;
            case 2: positions = new Vector2[] { new Vector2(-0.36f, -0.36f), new Vector2(0.36f, 0.36f) }; break;
            case 3: positions = new Vector2[] { new Vector2(-0.36f, -0.36f), Vector2.zero, new Vector2(0.36f, 0.36f) }; break;
            case 4: positions = new Vector2[] { new Vector2(-0.36f, -0.36f), new Vector2(-0.36f, 0.36f), new Vector2(0.36f, -0.36f), new Vector2(0.36f, 0.36f) }; break;
            case 5: positions = new Vector2[] { new Vector2(-0.36f, -0.36f), new Vector2(-0.36f, 0.36f), Vector2.zero, new Vector2(0.36f, -0.36f), new Vector2(0.36f, 0.36f) }; break;
            case 6: positions = new Vector2[] { new Vector2(-0.36f, -0.36f), new Vector2(0.36f, 0f), new Vector2(0.36f, -0.36f), new Vector2(-0.36f, 0.36f), new Vector2(-0.36f, 0f), new Vector2(0.36f, 0.36f) }; break;
        }

        for (int i = 0; i < positions.Length; i++)
        {
            dots[i].localPosition = positions[i];
        }
    }

    void OnDisable()
    {
        serializeDiceData = null;
        spriteRenderer.sprite = null;
        SetDots(0);
        for (int i = 0; i < Utils.MAX_DICE_LEVEL; i++)
        {
            dots[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void OnMouseDown()
    {
        order.SetMostFrontOrder(true);
    }

    public void OnMouseDrag()
    {
        transform.position = Utils.Mousepos;
    }

    public void OnMouseUp()
    {     
        MoveTransform(GameManager.Inst.GetspawnPositions(serializeDiceData.index), true, 0.2f, () => order.SetMostFrontOrder(false));

        GameObject[] raycastAll = GameManager.Inst.GetRaycastAll(Utils.DICE_LAYER);
        GameObject targetDiceObj = Array.Find(raycastAll, x => x.gameObject != gameObject);
        
        if (targetDiceObj != null)
        {
            var targetDice = targetDiceObj.GetComponent<Dice>();
            if(serializeDiceData.code == targetDice.serializeDiceData.code && 
               serializeDiceData.level == targetDice.serializeDiceData.level)
            {
                int nextLevel = serializeDiceData.level + 1;
                if (nextLevel > Utils.MAX_DICE_LEVEL)
                    return;
                
                var targetSerializeDiceData = targetDice.serializeDiceData;
                targetSerializeDiceData.code = GameManager.Inst.diceSo.GetRandomDiceData().code;
                targetSerializeDiceData.level = nextLevel;
                targetDice.SetupSlot(targetSerializeDiceData);
                gameObject.SetActive(false);

                //근희 테스트 
                GameManager.Inst.serializeDiceDatas[curIndex].isFull = false;
                //근희 테스트 
            }
        }
    }
    
    void MoveTransform(Vector2 targetpos, bool useDotween, float duration = 0f, TweenCallback action = null) 
    {
        if (useDotween)
        {
            transform.DOMove(targetpos, duration).OnComplete(action); 
        }
        else
        {
            transform.position = targetpos;
        }
    }

    IEnumerator DiceBulletSpawnCo()
    {
        while (true)
        {
            Enemy targetEnemy = null;

            if (GameManager.Inst.enemies.Count > 0)
            {
                targetEnemy = GameManager.Inst.enemies[0];
            }

            if (targetEnemy != null)
            {
                var diceBulletObj = ObjectPooler.Inst.SpawnFromPool("diceBullet", dots[0].position, Utils.QI);
                diceBulletObj.GetComponent<DiceBullet>().SetupDiceBullet(serializeDiceData, targetEnemy);
            }
            yield return Utils.delayAttack;
        }
    }
}
