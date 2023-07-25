using System;
using System.Collections;
using System.Collections.Generic;
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


public class Utils
{
    public const int MAX_DICE_LEVEL = 6;
    public const int DICE_LAYER = 6;

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

    public static GameObject[] GetRaycastAll(int layerMask)
    {
        var mousePos = Mousepos;
        mousePos.z = -100f;
        var raycastHit2D = Physics2D.RaycastAll(mousePos, Vector3.forward, float.MaxValue, 1 << layerMask);
        return Array.ConvertAll(raycastHit2D, x => x.collider.gameObject);
    }

    public static Vector2[] positions(int level) =>
        level switch
        {
            1 => new Vector2[] { Vector2.zero },
            2 => new Vector2[] { new Vector2(-0.36f, -0.36f), new Vector2(0.36f, 0.36f) },
            3 => new Vector2[] { new Vector2(-0.36f, -0.36f), Vector2.zero, new Vector2(0.36f, 0.36f) },
            4 => new Vector2[] { new Vector2(-0.36f, -0.36f), new Vector2(-0.36f, 0.36f), new Vector2(0.36f, -0.36f), new Vector2(0.36f, 0.36f) },
            5 => new Vector2[] { new Vector2(-0.36f, -0.36f), new Vector2(-0.36f, 0.36f), Vector2.zero, new Vector2(0.36f, -0.36f), new Vector2(0.36f, 0.36f) },
            6 => new Vector2[] { new Vector2(-0.36f, -0.36f), new Vector2(0.36f, 0f), new Vector2(0.36f, -0.36f), new Vector2(-0.36f, 0.36f), new Vector2(-0.36f, 0f), new Vector2(0.36f, 0.36f) },
            _ => new Vector2[] { Vector2.zero },
        };
}
