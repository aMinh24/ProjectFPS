using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPackage : MonoBehaviour
{
    public Transform[] pack;
    private void Awake()
    {
        foreach (var p in pack)
        {
            p.SetParent(null);
        }
    }
}
