using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    [Header("Conponents")]
    [SerializeField] SpriteRenderer spriteRender;

    [Header("Values")]
    [SerializeField] DiceDate diceDate;
    [SerializeField] Transform[] dots;
    [SerializeField] int level; //1~6±îÁö

    public void SetupSlot(DiceDate diceDate)
    {
        this.diceDate = diceDate;
        spriteRender.sprite = diceDate.sprite; 
    }

    public void SetDots(int level)
    {
        for (int i = 0; i < Utils.MAX_DICE_LEVEL; i++)
        {
           dots[i].gameObject.SetActive(i < level);
        }
    }

    void Start()
    {
        SetDots(1);
    }
}
