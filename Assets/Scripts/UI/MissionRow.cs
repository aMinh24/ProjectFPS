using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200005F RID: 95
public class MissionRow : MonoBehaviour
{
	// Token: 0x060001A1 RID: 417 RVA: 0x00009A1C File Offset: 0x00007C1C
	public void Init(object value)
	{
		if (value != null)
		{
			DailyMissionData dailyMissionData = value as DailyMissionData;
			if (dailyMissionData != null)
			{
				this.claimButton.gameObject.SetActive(dailyMissionData.isFinished && !dailyMissionData.isClaimed);
				this.tickImg.SetActive(dailyMissionData.isClaimed);
				this.idMission = dailyMissionData.id;
				this.missionDetail.text = string.Concat(new string[]
				{
					dailyMissionData.missionName,
					": Kill ",
					dailyMissionData.killTarget.ToString(),
					" enemies in ",
					dailyMissionData.timeTarget.ToString(),
					"m"
				});
			}
			AchivementKill achivementKill = value as AchivementKill;
			if (achivementKill != null)
			{
				this.claimButton.gameObject.SetActive(achivementKill.killTarget <= BaseManager<MissionManager>.Instance.totalKill && !achivementKill.isClaimed);
				this.tickImg.SetActive(achivementKill.isClaimed);
				this.idMission = achivementKill.id;
				this.missionDetail.text = string.Format("Reach {0} kills ({1}/{2})", achivementKill.killTarget, BaseManager<MissionManager>.Instance.totalKill, achivementKill.killTarget);
			}
			AchivementTime achivementTime = value as AchivementTime;
			if (achivementTime != null)
			{
				this.claimButton.gameObject.SetActive((double)achivementTime.timeTarget <= Math.Floor((double)(BaseManager<MissionManager>.Instance.timeOnline / 60f)) && !achivementTime.isClaimed);
				this.tickImg.SetActive(achivementTime.isClaimed);
				this.idMission = achivementTime.id;
				this.missionDetail.text = string.Format("Online {0} minutes ({1}/{2})", achivementTime.timeTarget, Math.Floor((double)(BaseManager<MissionManager>.Instance.timeOnline / 60f)), achivementTime.timeTarget);
			}
		}
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00009C0D File Offset: 0x00007E0D
	public void OnClaimButton()
	{
		if (BaseManager<ListenerManager>.HasInstance())
		{
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.ON_CLAIM_BUTTON, this.idMission);
		}
		this.claimButton.gameObject.SetActive(false);
		this.tickImg.SetActive(true);
	}

	// Token: 0x040001B9 RID: 441
	public int idMission;

	// Token: 0x040001BA RID: 442
	public Button claimButton;

	// Token: 0x040001BB RID: 443
	public GameObject tickImg;

	// Token: 0x040001BC RID: 444
	public Text missionDetail;
}
