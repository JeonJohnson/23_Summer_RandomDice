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

    public void SetupSlot(SerializeDiceData serializeDiceData)
    {
        this.serializeDiceData = serializeDiceData;
        var diceData = GameManager.Inst.diceSO.GetDiceDate(serializeDiceData.code);
        spriteRenderer.sprite = diceData.sprite;
        SetDots(serializeDiceData.level);

        for(int i = 0; i < Utils.MAX_DICE_LEVEL; i++)
        {
            dots[i].GetComponent<SpriteRenderer>().color = diceData.color;
        }
    }

    public void SetDots(int level)
    {
        for (int i = 0; i < Utils.MAX_DICE_LEVEL; i++)
        {
            dots[i].gameObject.SetActive(i < level);
        }

        //À§Ä¡
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
            if(serializeDiceData.code == targetDice.serializeDiceData.code
                && serializeDiceData.level == targetDice.serializeDiceData.level)
            {
                int nextLevel = serializeDiceData.level + 1;
                if (nextLevel > Utils.MAX_DICE_LEVEL)
                    return;
                
                var targetSerializeDiceData = targetDice.serializeDiceData;
                targetSerializeDiceData.code = GameManager.Inst.diceSO.GetRandomDiceData().code;
                targetSerializeDiceData.level = nextLevel;
                targetDice.SetupSlot(targetSerializeDiceData);
                gameObject.SetActive(false);
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
