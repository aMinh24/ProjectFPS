using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AchivementData", menuName = "Data/Achivement Data", order = 2)]
public class AchivementList : ScriptableObject
{
    public List<AchivementTime> TimeList;
    public List<AchivementKill> KillList;
}
[Serializable]
public class AchivementTime : requiredData
{
    //id [Range(100,199)]

    public float timeTarget = 0; //minute
}
[Serializable]
public class AchivementKill : requiredData
{
    // id [Range(200,299)]
    public int killTarget = 0;
}

