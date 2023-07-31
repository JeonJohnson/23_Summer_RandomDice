using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllScene : MonoBehaviour
{
    void Update()
    {
        Debug.Log(SceneManager.sceneCount);
        if (Input.anyKeyDown && SceneManager.sceneCount == 1)
        {
            LoadingScene.LoadScene("InGame");
        }
    }

    public void Retry() {
        LoadingScene.LoadScene("InGame");
    }
}
