using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour {

    public static SpriteRenderer[] hearts;

    void Awake() {
        hearts = GetComponentsInChildren<SpriteRenderer>();
    }

    public static void DrawHeart() {
        foreach(SpriteRenderer heart in hearts) {
            heart.gameObject.SetActive(true);
        }
        for(int i = 3; i > Player.instance.playerCurHealth; --i) {
            hearts[i -1].gameObject.SetActive(false);
        }
    }
}
