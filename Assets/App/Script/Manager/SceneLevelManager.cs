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
        SceneManager.sceneLoaded += LevelLoadComplete;
    }
    private void LevelLoadComplete(Scene arg0, LoadSceneMode arg1)
    {
        LevelLoadComplete();
        FindItems();
        Locator.GetService<Player>().IsLevelLoading = false;
    }
    private void LevelLoadComplete()
    {
        var player = Locator.GetService<Player>();
        player.transform.position = GameObject.FindWithTag("Respawn").transform.position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Locator.GetService<Player>().itemCount = 0;
    }

    public void LevelReload()
    {
        Debug.Log($"SceneLevelManager :: CurrentLevelLoad :: Scenelevel[CurrentLevel] : {Scenelevel[CurrentLevel]}");
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(Scenelevel[CurrentLevel]).completed += CurrentLevelLoad;
        }
        else
        {
            CurrentLevelLoad();
        }
    }
    public void MoveNextLevel()
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(Scenelevel[CurrentLevel]).completed += NextLevelLoad;
        }
        else
        {
            NextLevelLoad();
        }
    }
    public void SelectLevelLoad(int level)
    {
        Debug.Log($"SceneLevelManager :: CurrentLevelLoad :: Scenelevel[CurrentLevel] : {Scenelevel[CurrentLevel]}");
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(Scenelevel[CurrentLevel]).completed += (v) =>
            {
                CurrentLevel = level;
                CurrentLevelLoad();
            };
        }
        else
        {
            CurrentLevelLoad();
        }
    }
    private void FindItems()
    {
        CurrentItemCount = GameObject.FindGameObjectsWithTag("Item").Length;
    }


    private void CurrentLevelLoad(AsyncOperation asyncOperation = null)
    {
        if (CurrentLevel < Scenelevel.Length)
            SceneManager.LoadScene(Scenelevel[CurrentLevel], LoadSceneMode.Additive);
    }
    private void NextLevelLoad(AsyncOperation asyncOperation = null)
    {
        CurrentLevel++;
        CurrentLevelLoad();
    }

}
