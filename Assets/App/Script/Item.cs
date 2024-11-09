using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().GetItemCall();
            gameObject.SetActive(false);
            SoundManager.Instance.PlayEffectSound(Const_Sound.Const_SFX.DM_CGS_28.ToString());
        }
    }
}
