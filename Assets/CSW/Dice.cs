using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Dice : MonoBehaviour
{
    [Header("Conponents")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Order order;

    [Header("Values")]
    [SerializeField] UnityEngine.Transform[] dots;
    public SerializeDiceData serializeDiceData { get; private set; }
    int dotCount;

    public DiceDate diceDate => GameManager.Inst.diceSO.GetDiceDate(serializeDiceData.code);

    public void SetupDice(SerializeDiceData serializeDiceData)
    {
        this.serializeDiceData = serializeDiceData;
        GameManager.Inst.SetSerializeDiceData(serializeDiceData);

        var diceData = GameManager.Inst.diceSO.GetDiceDate(serializeDiceData.code);
        spriteRenderer.sprite = diceData.sprite;
        SetDots(serializeDiceData.level);

        for (int i = 0; i < Utils.MAX_DICE_LEVEL; i++)
        {
            dots[i].GetComponent<SpriteRenderer>().color = diceData.color;
        }

        if (serializeDiceData.code == 0)
            gameObject.SetActive(false);
    }

    public void SetDots(int level)
    {
        Vector2[] position = Utils.positions(level);
        int dotCount = 0;

        for (int i = 0; i < Utils.MAX_DICE_LEVEL; i++)
        {
            dots[i].gameObject.SetActive(i < level);
            dots[i].localPosition = i < level ? position[i] : Vector2.zero;
            if (i < level)
                dotCount++;
        }
        this.dotCount = dotCount;
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
        MoveTransform(GameManager.Inst.diceSO.GetspawnPositions(serializeDiceData.index), true, 0.2f, () => order.SetMostFrontOrder(false));

        GameObject[] raycastAll = Utils.GetRaycastAll(Utils.DICE_LAYER);
        GameObject targetDiceObj = Array.Find(raycastAll, x => x.gameObject != gameObject);

        if (targetDiceObj != null)
        {
            var targetDice = targetDiceObj.GetComponent<Dice>();
            int nextLevel = serializeDiceData.level + 1;

            if (serializeDiceData.code == targetDice.serializeDiceData.code &&
                serializeDiceData.level == targetDice.serializeDiceData.level &&
                nextLevel <= Utils.MAX_DICE_LEVEL)
            {
                var targetSerializeDiceData = new SerializeDiceData(targetDice.serializeDiceData.index,
                    true, GameManager.Inst.diceSO.GetRandomDiceData().code, nextLevel);
                targetDice.SetupDice(targetSerializeDiceData);

                var curSerializeDiceData = new SerializeDiceData(serializeDiceData.index, false, 0, 0);
                SetupDice(curSerializeDiceData);
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
}
