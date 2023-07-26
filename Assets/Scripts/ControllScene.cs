using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllScene : MonoBehaviour {
    void Update() {
        if (Input.anyKeyDown) {
            LoadingScene.LoadScene("InGame");
        }
    }
}
