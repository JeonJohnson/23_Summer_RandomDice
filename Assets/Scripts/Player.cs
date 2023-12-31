using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public bool isLive = true;

    public int playerCurHealth;
    public int playerMaxHealth;

    public int money;

    public GameObject gameOver;

    void Awake()
    {
        instance = this;
    }

    public void takePlayerDamage(int dmg)
    {
        if (playerCurHealth - dmg <= 0)
        {
            playerCurHealth = 0;
            PlayerDead();
        }
        else
        {
            playerCurHealth -= dmg;
        }
        Heart.DrawHeart();
    }

    void OnEnable()
    {
        isLive = true;
        playerCurHealth = playerMaxHealth;
        Heart.DrawHeart();
    }

    void PlayerDead()
    {
        isLive = false;
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }
}
