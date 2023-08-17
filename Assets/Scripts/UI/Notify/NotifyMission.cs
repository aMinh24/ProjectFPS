using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifyMission : BaseNotify
{
    public Text notifyText;
    public override void Hide()
    {
        base.Hide();
    }

    public override void Init()
    {
        base.Init();
        
    }

    public override void Show(object data)
    {
        base.Show(data);
        if (data is string str)
        {
            notifyText.text = "Done "+str;
        }
        Invoke("Hide", 5f);
    }
}
