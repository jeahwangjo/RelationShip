using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] DontDestroyObject;
    [SerializeField]
    private GameObject player;
    [HideInInspector]
    public int CurrentItemCount;

    public string[] Scenelevel;
    public int CurrentLevel = 0;

    private void Awake()
    {
        Locator.RegisterService(this);
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
        LevelLoadComplete();
        FindItems();
    }

    public void LevelComplete()
    {
        CurrentLevel++;
        LoadLevel();
    }
    [Button]
    public void LoadLevel()
    {
        if (CurrentLevel < Scenelevel.Length)
            SceneManager.LoadScene(Scenelevel[CurrentLevel]);
    }

    private void LevelLoadComplete()
    {
        player.transform.position = GameObject.FindWithTag("Respawn").transform.position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Locator.GetService<JoystickPlayerExample>().itemCount = 0;
    }
    private void FindItems()
    {
        CurrentItemCount = GameObject.FindGameObjectsWithTag("Item").Length;
    }
}
