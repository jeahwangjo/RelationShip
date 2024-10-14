using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    #region Field
    [SerializeField] private SoundDataSO soundDataSO;
    [SerializeField] private GameObject AudioPrefab;

    private Transform activeAudio;
    private Transform deActiveAudio;
    private Dictionary<string, AudioSourceItem> ActiveAudioDic = new Dictionary<string, AudioSourceItem>();
    #endregion
    #region Initialize
    public void Awake()
    {
        Instance = this;
        InitComponent();
    }
    private void InitComponent()
    {
        if (activeAudio == null)
        {
            activeAudio = new GameObject("ActiveAudio").transform;
            activeAudio.parent = transform;
            deActiveAudio = new GameObject("DeActiveAudio").transform;
            deActiveAudio.parent = transform;
        }
    }
    #endregion
    #region Event
    private Action<AudioClip> OnExtensionSoundLoaded;
    #endregion
    #region Control
    public void PlayBackgroundSound(string clipName)
    {
        PlaySound(clipName, true);
    }
    public void PlayEffectSound(string clipName)
    {
        PlaySound(clipName, false);
    }
    public void StopSound(string clipName)
    {
        ReturnPoolItem(ActiveAudioDic[clipName]);
        ActiveAudioDic.Remove(clipName);
    }
    private void PlaySound(string clipName, bool isLoop)
    {
        AudioSourceItem poolItem = GetPoolItem(clipName);
        if (ActiveAudioDic.ContainsKey(clipName) == false)
        {
            ActiveAudioDic.Add(clipName, poolItem);
        }
        poolItem.Play(soundDataSO.dicAudio[clipName], isLoop);
    }
    private void PlayClipAtPoint(string clipName, Vector3 position)
    {
        GetPoolItem(clipName).PlayClipAtPoint(soundDataSO.dicAudio[clipName], position);
    }
    #endregion
    #region ObjectPool
    private AudioSourceItem GetPoolItem(string clipName)
    {
        Transform audioTransform;
        if (deActiveAudio.childCount > 0)
        {
            audioTransform = deActiveAudio.GetChild(0);
        }
        else
        {
            audioTransform = Instantiate(AudioPrefab).transform;
        }
        audioTransform.gameObject.SetActive(true);
        audioTransform.parent = activeAudio;

        AudioSourceItem audioSourceItem = audioTransform.GetComponent<AudioSourceItem>();
        audioSourceItem.gameObject.name = clipName;
        audioSourceItem.OnClipPlayEnd = ReturnPoolItem;
        return audioSourceItem;
    }
    private void ReturnPoolItem(AudioSourceItem audioSourceItem)
    {
        audioSourceItem.transform.parent = deActiveAudio;
        audioSourceItem.gameObject.SetActive(false);
        ActiveAudioDic.Remove(audioSourceItem.gameObject.name);
    }
    #endregion
    #region ExtendSound
    public void PlayExtendSound(string fileName)
    {
        string projectPath = Application.persistentDataPath;
        string folderPath = projectPath + "/SndData/ ";
        string mainUrl = folderPath + fileName;
        if (Application.platform == RuntimePlatform.Android)
        {
            //path = "jar:file://" + Application.dataPath + "!/assets/UnityData";
            mainUrl = "file://" + mainUrl;
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            mainUrl = "file://" + mainUrl;
        }
        else
        {
            //mainUrl = "file://" + mainUrl;
        }

        // StartCoroutine(LoadData_SoundPlayer(backgroundAudioSource, mainUrl));
    }
    IEnumerator LoadData_SoundPlayer(AudioSource source, string url)
    {
        Debug.Log($"LoadData_SoundPlayer : {url}");
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                OnExtensionSoundLoaded?.Invoke(DownloadHandlerAudioClip.GetContent(www));
            }
        }
    }
    #endregion
    #region Ext

    IEnumerator PlayCheckAndDestory(GameObject obj)
    {
        if (obj && obj.GetComponent<AudioSource>())
        {
            AudioSource audio = obj.GetComponent<AudioSource>();
            while (audio.isPlaying)
            {
                yield return new WaitForSeconds(0.25f);
            }
            Destroy(obj);
        }
    }
    #endregion
}
