
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class DataManager : BaseManager<DataManager>
{
	// Token: 0x0600010F RID: 271 RVA: 0x00006D8C File Offset: 0x00004F8C
	private void Start()
	{
		this.dataFilePath = Application.persistentDataPath + "/playerdata.json";
		this.missionFilePath = Application.persistentDataPath + "/mission.json";
		this.achivementFilePath = Application.persistentDataPath + "/achive.json";
		Debug.Log(achivementFilePath);
		this.LoadPlayerData();
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00006DDE File Offset: 0x00004FDE
	public List<DailyMissionData> GetMissions()
	{
		return Resources.LoadAll<MissionList>("Mission")[0].missionsData;
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00006DF1 File Offset: 0x00004FF1
	public AchivementList GetAchivementList()
	{
		return Resources.LoadAll<AchivementList>("Mission")[0];
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00006E00 File Offset: 0x00005000
	public Dictionary<KeyInfo, int> GetInfo(string nameWeapon)
	{
		Dictionary<KeyInfo, int> dictionary = new Dictionary<KeyInfo, int>();
		if (!(nameWeapon == "scar"))
		{
			if (!(nameWeapon == "pistol"))
			{
				if (nameWeapon == "tommy")
				{
					dictionary.Add(KeyInfo.maxAmmo, this.TommyInfo.totalAmmo);
					dictionary.Add(KeyInfo.fireRate, this.TommyInfo.fireRate);
					dictionary.Add(KeyInfo.bulletSpeed, this.TommyInfo.bulletSpeed);
					dictionary.Add(KeyInfo.damage, this.TommyInfo.damage);
				}
			}
			else
			{
				dictionary.Add(KeyInfo.maxAmmo, this.PistolInfo.totalAmmo);
				dictionary.Add(KeyInfo.fireRate, this.PistolInfo.fireRate);
				dictionary.Add(KeyInfo.bulletSpeed, this.PistolInfo.bulletSpeed);
				dictionary.Add(KeyInfo.damage, this.PistolInfo.damage);
			}
		}
		else
		{
			dictionary.Add(KeyInfo.maxAmmo, this.ScarInfo.totalAmmo);
			dictionary.Add(KeyInfo.fireRate, this.ScarInfo.fireRate);
			dictionary.Add(KeyInfo.bulletSpeed, this.ScarInfo.bulletSpeed);
			dictionary.Add(KeyInfo.damage, this.ScarInfo.damage);
		}
		return dictionary;
	}

	private string ReadDataSO(string path)
	{
		if (File.Exists(path))
		{

			return File.ReadAllText(path);

		}
		return null;
	}

	private void WriteDataSO(object data, string path)
	{
		string contents = JsonUtility.ToJson(data);
		if (!File.Exists(path))
		{
			FileStream f = File.Create(path);
			f.Close();
		}

		File.WriteAllText(path, contents);
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00006F60 File Offset: 0x00005160
	public void LoadPlayerData()
	{
		string text = this.ReadDataSO(this.dataFilePath);
		string text2 = this.ReadDataSO(this.missionFilePath);
		string text3 = this.ReadDataSO(this.achivementFilePath);
		if (text == null)
		{
			this.WriteDataSO(this.PlayerData, this.dataFilePath);
			text = this.ReadDataSO(this.dataFilePath);
		}
		if (text2 == null)
		{
			this.WriteDataSO(this.MissionList, this.missionFilePath);
			text2 = this.ReadDataSO(this.missionFilePath);
		}
		if (text3 == null)
		{
			this.WriteDataSO(this.AchivementList, this.achivementFilePath);
			text3 = this.ReadDataSO(this.achivementFilePath);
		}
		JsonUtility.FromJsonOverwrite(text, this.PlayerData);
		JsonUtility.FromJsonOverwrite(text2, this.MissionList);
		JsonUtility.FromJsonOverwrite(text3, this.AchivementList);
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000705D File Offset: 0x0000525D
	public void SavePlayerData()
	{
		this.WriteDataSO(this.PlayerData, this.dataFilePath);
		this.WriteDataSO(this.MissionList, this.missionFilePath);
		this.WriteDataSO(this.AchivementList, this.achivementFilePath);
	}

	// Token: 0x04000148 RID: 328
	public GunInfo ScarInfo;

	// Token: 0x04000149 RID: 329
	public GunInfo PistolInfo;

	// Token: 0x0400014A RID: 330
	public GunInfo TommyInfo;

	// Token: 0x0400014B RID: 331
	public GlobalConfig GlobalConfig;

	// Token: 0x0400014C RID: 332
	public PlayerDataSO PlayerData;

	// Token: 0x0400014D RID: 333
	public MissionList MissionList;

	// Token: 0x0400014E RID: 334
	public AchivementList AchivementList;

	// Token: 0x0400014F RID: 335
	private string dataFilePath;

	// Token: 0x04000150 RID: 336
	private string missionFilePath;

	// Token: 0x04000151 RID: 337
	private string achivementFilePath;
}
