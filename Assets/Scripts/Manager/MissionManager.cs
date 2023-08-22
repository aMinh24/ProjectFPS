using System;

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class MissionManager : BaseManager<MissionManager>
{
	// Token: 0x06000122 RID: 290 RVA: 0x000071E8 File Offset: 0x000053E8
	private void Start()
	{
		if (BaseManager<DataManager>.HasInstance())
		{
			//BaseManager<DataManager>.Instance.LoadPlayerData();
			this.missionList = BaseManager<DataManager>.Instance.GetMissions();
			AchivementList achivementList = BaseManager<DataManager>.Instance.GetAchivementList();
			this.achivementKillsList = achivementList.KillList;
			this.achivementTimeList = achivementList.TimeList;
			this.totalKill = BaseManager<DataManager>.Instance.PlayerData.totalKill;
			this.timeOnline = BaseManager<DataManager>.Instance.PlayerData.totalTime * 60f;
			Debug.Log("total time: " + this.timeOnline.ToString());
		}
		if (BaseManager<ListenerManager>.HasInstance())
		{
			BaseManager<ListenerManager>.Instance.Register(ListenType.ON_START_MISSION, new Action<object>(this.OnMissionStart));
			BaseManager<ListenerManager>.Instance.Register(ListenType.ON_END_MISSION, new Action<object>(this.OnMissionEnd));
			BaseManager<ListenerManager>.Instance.Register(ListenType.ON_END_MISSION, new Action<object>(this.updatePlayerData));
			BaseManager<ListenerManager>.Instance.Register(ListenType.ON_ALLY_KILL, new Action<object>(this.CheckMission));
			BaseManager<ListenerManager>.Instance.Register(ListenType.ON_CLAIM_BUTTON, new Action<object>(this.ClaimMission));
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00007305 File Offset: 0x00005505
	private void Update()
	{
		this.timeOnline += Time.deltaTime;
		if (this.isPlaying)
		{
			this.time += Time.deltaTime;
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00007333 File Offset: 0x00005533
	public void OnMissionStart(object data)
	{
		this.isPlaying = true;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000733C File Offset: 0x0000553C
	public void OnMissionEnd(object data)
	{
		this.isPlaying = false;
		this.time = 0f;
		this.kill = 0;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00007358 File Offset: 0x00005558
	public void CheckMission(object data)
	{
		if (this.isPlaying)
		{
			this.kill++;
			foreach (DailyMissionData dailyMissionData in this.missionList)
			{
				if (dailyMissionData.timeTarget * 60f >= this.time && dailyMissionData.killTarget <= (float)this.kill && !dailyMissionData.isFinished)
				{
					if (BaseManager<UIManager>.HasInstance())
					{
						BaseManager<UIManager>.Instance.ShowNotify<NotifyMission>(dailyMissionData.missionName, true);
					}
					Debug.Log("Done " + dailyMissionData.missionName);
					dailyMissionData.isFinished = true;
					if (BaseManager<DataManager>.HasInstance())
					{
						foreach (DailyMissionData dailyMissionData2 in BaseManager<DataManager>.Instance.MissionList.missionsData)
						{
							if (dailyMissionData2.id == dailyMissionData.id)
							{
								dailyMissionData2.isFinished = true;
							}
						}
					}
					return;
				}
			}
			this.totalKill++;
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000749C File Offset: 0x0000569C
	public void updatePlayerData(object data)
	{
		if (BaseManager<DataManager>.HasInstance())
		{
			BaseManager<DataManager>.Instance.PlayerData.totalKill = this.totalKill;
			BaseManager<DataManager>.Instance.PlayerData.totalTime = math.floor(this.timeOnline / 60f);
			BaseManager<DataManager>.Instance.SavePlayerData();
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x000074F0 File Offset: 0x000056F0
	public void ClaimMission(object data)
	{
		if (data is int)
		{
			int num = (int)data;
			List<requiredData> list = new List<requiredData>();
			if (num >= 200)
			{
				list = new List<requiredData>(this.achivementKillsList);
			}
			else if (num >= 100)
			{
				list = new List<requiredData>(this.achivementTimeList);
			}
			else
			{
				list = new List<requiredData>(this.missionList);
			}
			foreach (requiredData requiredData in list)
			{
				if (requiredData.id == num)
				{
					requiredData.isClaimed = true;
				}
			}
			if (BaseManager<DataManager>.HasInstance())
			{
				foreach (DailyMissionData dailyMissionData in BaseManager<DataManager>.Instance.MissionList.missionsData)
				{
					if (dailyMissionData.id == num && dailyMissionData.isFinished)
					{
						dailyMissionData.isClaimed = true;
					}
				}
				foreach (requiredData requiredData2 in BaseManager<DataManager>.Instance.AchivementList.KillList)
				{
					if (requiredData2.id == num)
					{
						requiredData2.isClaimed = true;
					}
				}
				foreach (requiredData requiredData3 in BaseManager<DataManager>.Instance.AchivementList.TimeList)
				{
					if (requiredData3.id == num)
					{
						requiredData3.isClaimed = true;
					}
				}
				BaseManager<DataManager>.Instance.SavePlayerData();
			}
		}
	}

	// Token: 0x06000129 RID: 297 RVA: 0x000076BC File Offset: 0x000058BC
	private void OnApplicationQuit()
	{
		this.updatePlayerData(null);
	}

	// Token: 0x04000153 RID: 339
	public List<DailyMissionData> missionList;

	// Token: 0x04000154 RID: 340
	public List<AchivementTime> achivementTimeList;

	// Token: 0x04000155 RID: 341
	public List<AchivementKill> achivementKillsList;

	// Token: 0x04000156 RID: 342
	private float time;

	// Token: 0x04000157 RID: 343
	private int kill;

	// Token: 0x04000158 RID: 344
	public int totalKill;

	// Token: 0x04000159 RID: 345
	public float timeOnline;

	// Token: 0x0400015A RID: 346
	private bool isPlaying;
}
