
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000031 RID: 49
[CreateAssetMenu(fileName = "MissionData", menuName = "Data/Mission Data", order = 3)]
public class MissionList : ScriptableObject
{
	// Token: 0x040000ED RID: 237
	public List<DailyMissionData> missionsData;
}
