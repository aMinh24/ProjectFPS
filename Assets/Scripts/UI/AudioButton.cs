
using UnityEngine;

public class AudioButton : MonoBehaviour
{
    public static void OnPointerEnter()
    {
        if (AudioManager.HasInstance())
        {
            AudioManager.Instance.PlaySE("OnPointerEnter");
        }
    }
    public static void OnPointerDown()
    {
        if (AudioManager.HasInstance())
        {
            AudioManager.Instance.PlaySE("OnPointerDown");
        }
    }
}
