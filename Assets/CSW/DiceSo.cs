using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DiceDate
{
    public int code;
    public Sprite sprite;
    public Color color;
    public int basicAttackDamage;
}

    [CreateAssetMenu(fileName = "DiceSO", menuName = "Scriptable Object/DiceSO")]

public class DiceSO : ScriptableObject
{
    public DiceDate[] diceDates;

    public DiceDate GetDiceDate(int code) => Array.Find(diceDates, x => x.code == code);

    public DiceDate GetRandomDiceData() => diceDates[UnityEngine.Random.Range(0, diceDates.Length)];
}
