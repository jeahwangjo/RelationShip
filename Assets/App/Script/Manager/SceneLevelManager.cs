using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLevelManager : MonoBehaviour
{
    [SerializeField]
    private string[] Scenelevel;

    #region [=== Field===]
    [HideInInspector]
    public int CurrentItemCount;
    public int CurrentLevel = 0;

    #endregion

    private void Awake()
    {
        Locator.RegisterService(this);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += NextLevel;
    }
    private void NextLevel(Scene arg0, LoadSceneMode arg1)
    {
        LevelLoadComplete();
        FindItems();
    }

    private void LevelLoadComplete()
    {
        var player = Locator.GetService<Player>();
        player.transform.position = GameObject.FindWithTag("Respawn").transform.position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Locator.GetService<Player>().itemCount = 0;
    }

    public void LevelComplete()
    {
        SceneManager.UnloadSceneAsync(Scenelevel[CurrentLevel]);
        CurrentLevel++;
        LoadLevel();
    }
    public void UnLoadLevel()
    {
        SceneManager.UnloadSceneAsync(Scenelevel[CurrentLevel]);
    }

    private void FindItems()
    {
        CurrentItemCount = GameObject.FindGameObjectsWithTag("Item").Length;
    }
    public void LoadLevel()
    {
        if (CurrentLevel < Scenelevel.Length)
            SceneManager.LoadScene(Scenelevel[CurrentLevel], LoadSceneMode.Additive);
    }
}
