using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEngine : Singleton<SceneEngine>
{
    public void Start() {
        Object.DontDestroyOnLoad(this.gameObject);
    }

    public void SetScene(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}
