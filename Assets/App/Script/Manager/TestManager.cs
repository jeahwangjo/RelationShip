using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public int selectLevel;

    [Button]
    private void MoveLevel()
    {
        var sceneLevelManager = Locator.GetService<SceneLevelManager>();
        sceneLevelManager.SelectLevelLoad(Mathf.Abs(selectLevel - 1));
    }
}
