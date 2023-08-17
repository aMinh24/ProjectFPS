using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConfirmBox : BaseOverlap
{
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
    }
    public void OnCancelButton()
    {
        Hide();
    }
    public void OnConfirmButton()
    {
#if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#else
        
        Application.Quit();
#endif
    }
}
