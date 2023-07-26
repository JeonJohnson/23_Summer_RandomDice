using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

[System.Serializable]
public class SerializeDiceData
{
    public int index;
    public bool isFull;
    public int code;
    public int level;

    public SerializeDiceData(int index, bool isFull, int code, int level)
    { 
        this.index = index;
        this.isFull = isFull;
        this.code = code;
        this.level = level;
    }
}


[System.Serializable]
public class Tile
{
    public Vector2 startPos;
}

public class Utils
{
    public const int MAX_DICE_LEVEL = 6;

    public static readonly Quaternion QI = Quaternion.identity;

    public static Vector3 Mousepos
    {
        get
        {
            var result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            result.z = 0;
            return result;  
        }
    }
    public const int DICE_LAYER = 6;


   public static readonly WaitForSeconds delayAttack = new WaitForSeconds(0.5f);   
    
    public static int TotalAttackDamage(int basicAttackDamage, int level)
    {
        int result = basicAttackDamage + level * 3;
        return result;
    }
}
