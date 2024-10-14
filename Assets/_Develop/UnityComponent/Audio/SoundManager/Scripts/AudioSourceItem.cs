using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceItem : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public Action<AudioSourceItem> OnClipPlayEnd;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void Play(AudioClip audioClip, bool isLoop = false)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.loop = isLoop;
        audioSource.Play();
        if (isLoop == false)
        {
            StartCoroutine(ClipDelayCheck(audioClip.length));
        }
    }

    public void PlayClipAtPoint(AudioClip audioClip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, 2f);
    }

    IEnumerator ClipDelayCheck(float time)
    {
        yield return new WaitForSeconds(time);
        OnClipPlayEnd?.Invoke(this);
    }

}
