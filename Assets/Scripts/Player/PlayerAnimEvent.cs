using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    public void SEAudio(string name)
    {
        if (AudioManager.HasInstance())
        {
            AudioManager.Instance.PlaySE(name);
        }
    }
    //public void RunAudio()
    //{
    //    if (AudioManager.HasInstance())
    //    {
    //        AudioManager.Instance.PlaySE("Run");
    //    }
    //}
}
