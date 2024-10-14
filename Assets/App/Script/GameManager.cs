using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] DontDestroyObject;
    [SerializeField]
    private GameObject player;
    private void Awake()
    {
        foreach (var item in DontDestroyObject)
        {
            DontDestroyOnLoad(item);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += NextLevel;
        SceneManager.LoadScene("Level1");
    }



    private void NextLevel(Scene arg0, LoadSceneMode arg1)
    {
        NextLevel();
    }
    private void NextLevel()
    {
        player.transform.position = GameObject.FindWithTag("Respawn").transform.position;
    }
}
