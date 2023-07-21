using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] DiceSO diceSO;
    [SerializeField] Vector2[] spawnPositions;
   
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Keypad0))
        {
            RandomSpawn();
        }
    }

    public void RandomSpawn()
    {
        int randIndex = Random.Range(0, spawnPositions.Length);
        Vector3 randPos = spawnPositions[randIndex];
        ObjectPooler.Inst.SpawnFromPool("slot", randPos, Utils.QI);
    }
}
