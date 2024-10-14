//SAO 버튼 추가
//현재 폴더에서 파일 가져오기.
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataSO", menuName = "ScriptableObjects/SoundDataSO", order = 1)]
public class SoundDataSO : ScriptableObject
{
    #region Field
    public string[] audioPath;
    public SerializeDictionary<string, AudioClip> dicAudio = new SerializeDictionary<string, AudioClip>();
    private SoundDataSO instance;
    public SoundDataSO Instance
    {
        get
        {
            if (instance == null)
            {
                instance = this;
            }
            return instance;
        }
    }
    #endregion
    #region Initailize
#if UNITY_EDITOR
    public void InitData()
    {
        dicAudio.Clear();
        FindDirInfo(audioPath);
    }
    private void FindDirInfo(string[] pathArr)
    {
        foreach (var assetURL in pathArr)
        {
            Debug.Log($"SoundDataSO :: InitData :: Application.dataPath  : {Application.dataPath + "\\" + assetURL}");
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Application.dataPath + "\\" + assetURL);
            SetFileToLst(di, assetURL);
        }
    }
    void SetFileToLst(System.IO.DirectoryInfo di, string url)
    {
        if (di == null) return;
        foreach (System.IO.FileInfo File in di.GetFiles())
        {
            //File.Name.Log();
            var extension = File.Extension;
            if (extension.Equals(".mp3") || extension.Equals(".wav"))
            {
                var clip = (AudioClip)AssetDatabase.LoadAssetAtPath($"Assets/{url}/{File.Name}", typeof(AudioClip));
                dicAudio.Add(File.Name.Substring(0, File.Name.IndexOf(File.Extension)), clip);
            }
        }
        EditorUtility.SetDirty(this);
        AssetDatabase.Refresh();
    }
#endif
    #endregion
}