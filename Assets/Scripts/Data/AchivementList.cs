
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000021 RID: 33
[CreateAssetMenu(fileName = "AchivementData", menuName = "Data/Achivement Data", order = 2)]
public class AchivementList : ScriptableObject
{
	// Token: 0x0400008F RID: 143
	public List<AchivementTime> TimeList;

	// Token: 0x04000090 RID: 144
	public List<AchivementKill> KillList;
}
