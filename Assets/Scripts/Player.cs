using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool isLive = true;

    public int playerCurHealth;
    public int playerMaxHealth;

    public int money;

    public void takePlayerDamage(int dmg) {
        if (playerCurHealth - dmg <= 0) {
            playerCurHealth = 0;
            PlayerDead();
        } else {
            playerCurHealth -= dmg;
        }
    }

    void PlayerDead() {
        isLive = false;
    }
}
