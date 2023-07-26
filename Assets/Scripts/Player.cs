using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
public class Player : MonoBehaviour {

    public static Player instance;

    public bool isLive = true;
    public int wave = 0;

    public int playerCurHealth;
    public int playerMaxHealth;
=======
public class Player : MonoBehaviour
{
    public static Player instance;

    public int currentHealth;
    public int maxHealth;
>>>>>>> Stashed changes

    public int money;

    void Awake() {
        instance = this;
    }

<<<<<<< Updated upstream
    public void takePlayerDamage(int dmg) {
        if (playerCurHealth - dmg <= 0) {
            playerCurHealth = 0;
            PlayerDead();
        } else {
            playerCurHealth -= dmg;
        }
    }

    void OnEnable() {
        isLive = true;
        playerCurHealth = playerMaxHealth;
    }

    void PlayerDead() {
        isLive = false;
        Time.timeScale = 0;
=======
    void Start() {
        
    }

    void Update() {
        
    }

    public void SubtractHealth() {

>>>>>>> Stashed changes
    }
}
