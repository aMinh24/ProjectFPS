
using Cinemachine;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class WeaponRecoilMulti : MonoBehaviour
{
	// Token: 0x0600020D RID: 525 RVA: 0x0000B5C6 File Offset: 0x000097C6
	private void Awake()
	{
		this.cameraShake = base.GetComponent<CinemachineImpulseSource>();
	}
    private void Start()
    {
		cam = characterAiming.mainCamera;
    }

    // Token: 0x0600020E RID: 526 RVA: 0x0000B5D4 File Offset: 0x000097D4
    public void GenerateRecoil(string weaponName)
	{
		this.time = this.duration;
		if (this.cam.enabled)
		{
			this.cameraShake.GenerateImpulse(characterAiming.mainCamera.transform.forward);
			this.rigController.Play("weapon_recoil_" + weaponName, 1, 0f);
			this.duration = 0.5f;
		}
		else
		{
			this.duration = 0.25f;
		}
		this.horizontalRecoil = this.recoilPattern[this.index].x;
		this.verticleRecoil = this.recoilPattern[this.index].y;
		this.index = this.NextIndex(this.index);
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000B692 File Offset: 0x00009892
	public void Reset()
	{
		this.index = 0;
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000B69B File Offset: 0x0000989B
	private int NextIndex(int index)
	{
		return (index + 1) % this.recoilPattern.Length;
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000B6AC File Offset: 0x000098AC
	private void Update()
	{
		if (this.time > 0f)
		{
			CharacterAimingMultiplayer characterAiming = this.characterAiming;
			characterAiming.yAxist.Value = characterAiming.yAxist.Value - this.verticleRecoil / 10f * Time.deltaTime / this.duration * this.recoilModifier;
			CharacterAimingMultiplayer characterAiming2 = this.characterAiming;
			characterAiming2.xAxist.Value = characterAiming2.xAxist.Value - this.horizontalRecoil / 10f * Time.deltaTime / this.duration * this.recoilModifier;
			this.time -= Time.deltaTime;
		}
	}

	// Token: 0x0400020D RID: 525
	[HideInInspector]
	public CharacterAimingMultiplayer characterAiming;

	// Token: 0x0400020E RID: 526
	[HideInInspector]
	public CinemachineImpulseSource cameraShake;

	// Token: 0x0400020F RID: 527
	[HideInInspector]
	public Animator rigController;

	// Token: 0x04000210 RID: 528
	private float verticleRecoil;

	// Token: 0x04000211 RID: 529
	private float horizontalRecoil;

	// Token: 0x04000212 RID: 530
	public float duration;

	// Token: 0x04000213 RID: 531
	public float time;

	// Token: 0x04000214 RID: 532
	private int index;

	// Token: 0x04000215 RID: 533
	public Vector2[] recoilPattern;

	// Token: 0x04000216 RID: 534
	public float recoilModifier;

	// Token: 0x04000217 RID: 535
	public Camera cam;
}
