using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class _AnimationEvent : UnityEvent<string>
{

}
public class WeaponAnimationEvent : MonoBehaviour
{
    public _AnimationEvent WeaponAnimEvent = new _AnimationEvent();

    public void OnAnimationEvent(string eventName)
    {
        WeaponAnimEvent.Invoke(eventName);

    }
    public void OnHolsterEvent(string eventName)
    {
        WeaponAnimEvent.Invoke(eventName);
    }
    public void OnAudioEvent(string audioName)
    {
        if (AudioManager.HasInstance())
        {
            AudioManager.Instance.PlaySE(audioName);
        }
    }
}
