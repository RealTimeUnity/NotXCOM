﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePreLoader : MonoBehaviour {

    private const string PRELOAD_SCENE_NAME = "preload";

    public static ScenePreLoader Singleton;
    private string nextScene;

    public void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene(PRELOAD_SCENE_NAME);
    }
    public void FinishLoad()
    {
        SceneManager.LoadScene(nextScene);
    }
}