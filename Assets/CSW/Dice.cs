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
    [SerializeField] int level; //1~6까지

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

    void Start()
    {
        SetDots(1);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
            SetDots(1);
        if (Input.GetKeyDown(KeyCode.Keypad2))
            SetDots(2);
        if (Input.GetKeyDown(KeyCode.Keypad3))
            SetDots(3); 
        if (Input.GetKeyDown(KeyCode.Keypad4))
            SetDots(4);
        if (Input.GetKeyDown(KeyCode.Keypad5))
            SetDots(5);
        if (Input.GetKeyDown(KeyCode.Keypad6))
            SetDots(6);
    }
}