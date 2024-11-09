using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Locator.RegisterService(this);

    }
    private void Start()
    {
        var sceneLevelManager = Locator.GetService<SceneLevelManager>();
        sceneLevelManager.CurrentLevel = 0;
        sceneLevelManager.LoadLevel();
    }

}
