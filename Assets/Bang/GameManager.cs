using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] DiceSO diceSO;
    [SerializeField] Dice testDice;
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Keypad0))
        {
            testDice.SetupSlot(diceSO.GetDiceDate(2));
        }
    }
}
