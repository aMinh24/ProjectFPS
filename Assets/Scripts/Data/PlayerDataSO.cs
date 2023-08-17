using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data", order = 1)]
public class PlayerDataSO : ScriptableObject
{
    public int totalKill = 0;
    public float totalTime = 0;
}
