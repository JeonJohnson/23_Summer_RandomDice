using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    void Awake() => Inst = this;

    public DiceSO diceSO;
    [SerializeField] Vector2[] spawnPositions;
    [SerializeField] SerializeDiceData[] serializeDiceDatas; //��� �ֻ��� ���� ����ȭ 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            TryRandomSpawn(1);
        }
    }

    public bool TryRandomSpawn(int level = 1)
    {
        var emptyserializeDiceData = Array.FindAll(serializeDiceDatas, x => x.isFull == false);

        if (emptyserializeDiceData.Length <= 0)
            return false;
        
        int randIndex = emptyserializeDiceData[Random.Range(0, emptyserializeDiceData.Length)].index;
        Vector3 randPos = spawnPositions[randIndex];
        var randDiceData = diceSO.GetRandomDiceData();
        var dice = ObjectPooler.Inst.SpawnFromPool("dice", randPos, Utils.QI).GetComponent<Dice>();

        var serializeDiceData = new SerializeDiceData(randIndex, true, diceSO.GetRandomDiceData().code, level);
        dice.SetupSlot(serializeDiceData);
        serializeDiceDatas[randIndex] = serializeDiceData;

        return true;
    }

    public void SpawnBtnClick()
    {
        TryRandomSpawn(1);
    }

    public Vector2 GetspawnPositions(int index) => spawnPositions[index];
<<<<<<< Updated upstream

    public GameObject[] GetRaycastAll(int layerMask)
    {      
        var mousePos = Utils.Mousepos;
        mousePos.z = -100f;
        RaycastHit2D[] raycastHit2Ds = Physics2D.RaycastAll(mousePos, Vector3.forward, float.MaxValue, 1 << layerMask);
        var results = Array.ConvertAll(raycastHit2Ds, x => x.collider.gameObject);
        return results;          
    }
=======
>>>>>>> Stashed changes
}
