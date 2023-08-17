using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class requiredData
{
    public int id;
    public bool isClaimed = false;
}
[CreateAssetMenu(fileName = "MissionData", menuName = "Data/Mission Data", order = 3)]
public class MissionList : ScriptableObject
{
    public List<DailyMissionData> missionsData;
}
[Serializable]
public class DailyMissionData :requiredData
{ 
    //id (0-99)
    public string missionName;
    public bool isFinished = false;
    public float timeTarget;
    public float killTarget;
    public float headshotTarget;
}
