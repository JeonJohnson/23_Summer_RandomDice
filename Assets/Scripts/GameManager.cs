using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    void Awake() => Inst = this;

    public DiceSO diceSO;
    [SerializeField] SerializeDiceData[] serializeDiceDatas; //모든 주사위 정보 직렬화   
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        { 
        }
    }

    public void SetSerializeDiceData(SerializeDiceData serializeDiceData)
    {
        
    }

    public bool TryRandomSpawn(int level = 1)
    {
        var emptyserializeDiceData = Array.FindAll(serializeDiceDatas, x => x.isFull == false);
        if (emptyserializeDiceData.Length <= 0)
            return false;
        
        int randIndex = emptyserializeDiceData[Random.Range(0, emptyserializeDiceData.Length)].index;
        var diceObj = ObjectPooler.Inst.SpawnFromPool("dice", diceSO.GetspawnPositions(randIndex), Utils.QI);

        var serializeDiceData = new SerializeDiceData(randIndex, true, diceSO.GetRandomDiceData().code, level);
        diceObj.GetComponent<Dice>().SetupDice(serializeDiceData);

        return true;
    }

    public void SpawnBtnClick()
    {
        TryRandomSpawn(1);
    }
}
