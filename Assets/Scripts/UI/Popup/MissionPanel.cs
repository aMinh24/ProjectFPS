using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

// Token: 0x02000065 RID: 101
public class MissionPanel : BasePopup
{
	// Token: 0x060001C4 RID: 452 RVA: 0x0000A04F File Offset: 0x0000824F
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000A057 File Offset: 0x00008257
	public override void Init()
	{
		base.Init();
		this.rows = new List<MissionRow>();
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000A06C File Offset: 0x0000826C
	public override void Show(object data)
	{
		base.Show(data);
		foreach (MissionRow missionRow in this.rows)
		{
			Object.Destroy(missionRow.gameObject);
		}
		this.rows.Clear();
		if (BaseManager<MissionManager>.HasInstance())
		{
			foreach (DailyMissionData dailyMissionData in BaseManager<MissionManager>.Instance.missionList)
			{
				MissionRow missionRow2 = Object.Instantiate<MissionRow>(this.prf, this.content[0], false);
				if (!this.rows.Contains(missionRow2))
				{
					missionRow2.Init(dailyMissionData);
					if (dailyMissionData.isFinished)
					{
						missionRow2.gameObject.transform.SetAsFirstSibling();
					}
					this.rows.Add(missionRow2);
				}
			}
			foreach (AchivementKill achivementKill in BaseManager<MissionManager>.Instance.achivementKillsList)
			{
				MissionRow missionRow3 = Object.Instantiate<MissionRow>(this.prf, this.content[1], false);
				if (!this.rows.Contains(missionRow3))
				{
					missionRow3.Init(achivementKill);
					if (achivementKill.killTarget <= BaseManager<MissionManager>.Instance.totalKill && !achivementKill.isClaimed)
					{
						missionRow3.gameObject.transform.SetAsFirstSibling();
					}
					this.rows.Add(missionRow3);
				}
			}
			foreach (AchivementTime achivementTime in BaseManager<MissionManager>.Instance.achivementTimeList)
			{
				MissionRow missionRow4 = Object.Instantiate<MissionRow>(this.prf, this.content[1], false);
				if (!this.rows.Contains(missionRow4))
				{
					missionRow4.Init(achivementTime);
					if ((double)achivementTime.timeTarget <= Math.Floor((double)(BaseManager<MissionManager>.Instance.timeOnline / 60f)) && !achivementTime.isClaimed)
					{
						missionRow4.gameObject.transform.SetAsFirstSibling();
					}
					this.rows.Add(missionRow4);
				}
			}
		}
		this.OpenTab(0);
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000A2D4 File Offset: 0x000084D4
	public void OnCloseButton()
	{
		this.Hide();
		Debug.Log("hide panel");
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000A2E8 File Offset: 0x000084E8
	public void OpenTab(int n)
	{
		for (int i = 0; i < this.tabs.Count; i++)
		{
			this.tabs[i].TurnOff();
		}
		this.tabs[n].TurnOn();
	}

	// Token: 0x040001C2 RID: 450
	public MissionRow prf;

	// Token: 0x040001C3 RID: 451
	public Transform[] content;

	// Token: 0x040001C4 RID: 452
	private List<MissionRow> rows;

	// Token: 0x040001C5 RID: 453
	public List<TabMission> tabs;
}
