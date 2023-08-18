﻿
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class AudioManager : BaseManager<AudioManager>
{
	// Token: 0x060000F6 RID: 246 RVA: 0x000066F4 File Offset: 0x000048F4
	protected override void Awake()
	{
		base.Awake();
		this.bgmDic = new Dictionary<string, AudioClip>();
		this.seDic = new Dictionary<string, AudioClip>();
		this.gtDic = new Dictionary<string, AudioClip>();
		object[] array = Resources.LoadAll("Audio/BGM");
		object[] array2 = array;
		array = Resources.LoadAll("Audio/GTHEME");
		object[] array3 = array;
		array = Resources.LoadAll("Audio/SE");
		object[] array4 = array;
		foreach (AudioClip audioClip in array2)
		{
			this.bgmDic[audioClip.name] = audioClip;
		}
		foreach (AudioClip audioClip2 in array4)
		{
			this.seDic[audioClip2.name] = audioClip2;
		}
		foreach (AudioClip audioClip3 in array3)
		{
			this.gtDic[audioClip3.name] = audioClip3;
		}
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x000067D8 File Offset: 0x000049D8
	private void Start()
	{
		this.AttachBGMSource.volume = PlayerPrefs.GetFloat("BGM_VOLUME_KEY", 0.2f);
		this.AttachGTSource.volume = PlayerPrefs.GetFloat("BGM_VOLUME_KEY", 0.2f);
		this.AttachSESource.volume = PlayerPrefs.GetFloat("SE_VOLUME_KEY", 1f);
		if (!this.AttachBGMSource.isPlaying)
		{
			this.PlayBGM("BGM" + this.currentBGM.ToString(), 0.9f);
		}
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00006860 File Offset: 0x00004A60
	public void PlaySE(string seName, float delay = 0f)
	{
		if (!this.seDic.ContainsKey(seName))
		{
			Debug.LogError(seName + " There is no SE named");
			return;
		}
		this.nextSEName = seName;
		base.Invoke("DelayPlaySE", delay);
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00006894 File Offset: 0x00004A94
	public AudioClip GetAudioClip(string seName)
	{
		if (this.seDic.ContainsKey(seName))
		{
			return this.seDic[seName];
		}
		return null;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000068B2 File Offset: 0x00004AB2
	private void DelayPlaySE()
	{
		this.AttachSESource.PlayOneShot(this.seDic[this.nextSEName]);
	}

	// Token: 0x060000FB RID: 251 RVA: 0x000068D0 File Offset: 0x00004AD0
	public void PlayBGM(string bgmName, float fadeSpeedRate = 0.9f)
	{
		if (!this.bgmDic.ContainsKey(bgmName))
		{
			Debug.LogError(bgmName + " There is no BGM named");
			return;
		}
		if (!this.AttachBGMSource.isPlaying)
		{
			this.nextBGMName = "BGM" + ((this.currentBGM + 1) % this.bgmDic.Count).ToString();
			this.AttachBGMSource.clip = this.bgmDic[bgmName];
			this.AttachBGMSource.Play();
			return;
		}
		if (this.AttachBGMSource.clip.name != bgmName)
		{
			this.nextBGMName = bgmName;
			this.FadeOutBGM(fadeSpeedRate);
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00006980 File Offset: 0x00004B80
	public void PlayGT()
	{
		this.nextGTName = "GTHEME" + this.currentGT.ToString();
		this.AttachGTSource.clip = this.gtDic[this.nextGTName];
		this.AttachGTSource.volume = PlayerPrefs.GetFloat("BGM_VOLUME_KEY", 0.2f);
		this.AttachGTSource.Play();
		this.currentGT = (this.currentGT + 1) % this.gtDic.Count;
		this.isPlayingGT = true;
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00006A0A File Offset: 0x00004C0A
	public void StopGT()
	{
		this.isPlayingGT = false;
		this.AttachGTSource.Stop();
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00006A1E File Offset: 0x00004C1E
	public void FadeOutBGM(float fadeSpeedRate = 0.3f)
	{
		this.bgmFadeSpeedRate = fadeSpeedRate;
		this.isFadeOut = true;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00006A30 File Offset: 0x00004C30
	private void Update()
	{
		if (this.isPlayingGT)
		{
			if (this.AttachGTSource.isPlaying)
			{
				if (this.AttachGTSource.clip.length - this.AttachGTSource.time < 1f)
				{
					this.AttachGTSource.volume -= 0.3f * Time.deltaTime;
				}
			}
			else
			{
				this.PlayGT();
			}
		}
		if (this.AttachBGMSource != null && this.AttachBGMSource.clip.length - this.AttachBGMSource.time <= 1f)
		{
			this.FadeOutBGM(0.3f);
		}
		if (!this.isFadeOut)
		{
			return;
		}
		this.AttachBGMSource.volume -= Time.deltaTime * this.bgmFadeSpeedRate;
		if (this.AttachBGMSource.volume <= 0f)
		{
			this.AttachBGMSource.Stop();
			this.AttachBGMSource.volume = PlayerPrefs.GetFloat("BGM_VOLUME_KEY", 0.2f);
			this.isFadeOut = false;
			if (!string.IsNullOrEmpty(this.nextBGMName))
			{
				this.PlayBGM(this.nextBGMName, 0.9f);
			}
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00006B59 File Offset: 0x00004D59
	public void ChangeBGMVolume(float BGMVolume)
	{
		this.AttachBGMSource.volume = BGMVolume;
		PlayerPrefs.SetFloat("BGM_VOLUME_KEY", BGMVolume);
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00006B72 File Offset: 0x00004D72
	public void ChangeSEVolume(float SEVolume)
	{
		this.AttachSESource.volume = SEVolume;
		PlayerPrefs.SetFloat("SE_VOLUME_KEY", SEVolume);
	}

	// Token: 0x04000135 RID: 309
	private float bgmFadeSpeedRate = 0.9f;

	// Token: 0x04000136 RID: 310
	private int currentBGM;

	// Token: 0x04000137 RID: 311
	private int currentGT;

	// Token: 0x04000138 RID: 312
	private string nextBGMName;

	// Token: 0x04000139 RID: 313
	private string nextSEName;

	// Token: 0x0400013A RID: 314
	private string nextGTName;

	// Token: 0x0400013B RID: 315
	private bool isFadeOut;

	// Token: 0x0400013C RID: 316
	private bool isPlayingGT;

	// Token: 0x0400013D RID: 317
	public AudioSource AttachBGMSource;

	// Token: 0x0400013E RID: 318
	public AudioSource AttachGTSource;

	// Token: 0x0400013F RID: 319
	public AudioSource AttachSESource;

	// Token: 0x04000140 RID: 320
	private Dictionary<string, AudioClip> bgmDic;

	// Token: 0x04000141 RID: 321
	private Dictionary<string, AudioClip> seDic;

	// Token: 0x04000142 RID: 322
	private Dictionary<string, AudioClip> gtDic;
}
