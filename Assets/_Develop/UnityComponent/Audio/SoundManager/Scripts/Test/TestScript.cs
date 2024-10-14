using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    private Const_Sound.Const_SFX const_Sound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaySound(const_Sound);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // PlaySound(Const_Sound.Const_SFX.univ0002);
        }
    }

    private void PlaySound(Const_Sound.Const_SFX sound)
    {
        SoundManager.Instance.PlayEffectSound(sound.ToString());
    }
}
