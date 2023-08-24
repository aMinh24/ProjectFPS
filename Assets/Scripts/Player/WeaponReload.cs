
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000072 RID: 114
public class WeaponReload : MonoBehaviour
{
	// Token: 0x06000213 RID: 531 RVA: 0x0000B748 File Offset: 0x00009948
	private void Start()
	{
		if (animationEvent != null)
		{
            this.animationEvent.WeaponAnimEvent.AddListener(new UnityAction<string>(this.OnAnimationEvent));
        }
		
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0000B768 File Offset: 0x00009968
	private void Update()
	{
		if (this.activeWeapon.characterAiming.isEcs)
		{
			return;
		}
		WeaponRaycast weaponRaycast = this.activeWeapon.GetActiveWeapon();
		if (weaponRaycast && (Input.GetKeyUp(KeyCode.R) || weaponRaycast.ammoCount <= 0) && weaponRaycast.totalAmmo > 0 && weaponRaycast.ammoCount < weaponRaycast.gunInfo[KeyInfo.maxAmmo] - 5)
		{
			this.isReloading = true;
			if (rigController != null)
			{
                this.rigController.SetTrigger("reload_weapon");
            }
			
		}
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000B7E4 File Offset: 0x000099E4
	private void OnAnimationEvent(string eventName)
	{
		if (eventName == "detach_magazine")
		{
			this.DetachMagazine();
			return;
		}
		if (eventName == "drop_magazine")
		{
			this.DropMagazine();
			return;
		}
		if (eventName == "refill_magazine")
		{
			this.RefillMagazine();
			return;
		}
		if (eventName == "attach_magazine")
		{
			this.AttachMagazine();
			return;
		}
		if (eventName == "load_bullet")
		{
			this.LoadBullet();
			return;
		}
		if (!(eventName == "done_reload"))
		{
			return;
		}
		this.DoneReload();
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000B86C File Offset: 0x00009A6C
	private void DetachMagazine()
	{
		if (BaseManager<CameraManager>.HasInstance())
		{
			BaseManager<CameraManager>.Instance.TurnOffScope();
		}
		else
		{
			activeWeapon.camManager.TurnOffScope();
		}
		if (BaseManager<AudioManager>.HasInstance())
		{
			BaseManager<AudioManager>.Instance.PlaySE("PistolDetachMag", 0f);
		}
		WeaponRaycast weaponRaycast = this.activeWeapon.GetActiveWeapon();
		Transform component;
		if (!weaponRaycast.weaponName.Equals("pistol"))
		{
			component = this.leftHand;
		}
		else
		{
			component = weaponRaycast.magazine.GetComponent<Transform>();
		}
		this.magazineHand = Object.Instantiate<GameObject>(weaponRaycast.magazine, component, true);
		weaponRaycast.magazine.SetActive(false);
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000B8F8 File Offset: 0x00009AF8
	private void DropMagazine()
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.magazineHand, this.magazineHand.transform.position, this.magazineHand.transform.rotation);
		gameObject.transform.localScale = Vector3.one;
		gameObject.AddComponent<Rigidbody>();
		gameObject.AddComponent<BoxCollider>();
		Object.Destroy(gameObject, 3f);
		this.magazineHand.SetActive(false);
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0000B964 File Offset: 0x00009B64
	private void RefillMagazine()
	{
		this.magazineHand.SetActive(true);
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000B972 File Offset: 0x00009B72
	private void AttachMagazine()
	{
		if (BaseManager<AudioManager>.HasInstance())
		{
			BaseManager<AudioManager>.Instance.PlaySE("PistolAttachMag", 0f);
		}
		this.activeWeapon.GetActiveWeapon().magazine.SetActive(true);
		Object.Destroy(this.magazineHand);
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000B9B0 File Offset: 0x00009BB0
	private void LoadBullet()
	{
		WeaponRaycast weaponRaycast = this.activeWeapon.GetActiveWeapon();
		weaponRaycast.ammoCount = weaponRaycast.gunInfo[KeyInfo.maxAmmo];
		weaponRaycast.totalAmmo -= weaponRaycast.gunInfo[KeyInfo.maxAmmo];
		this.rigController.ResetTrigger("reload_weapon");
		if (BaseManager<ListenerManager>.HasInstance())
		{
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_AMMO, weaponRaycast);
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_TOTAL_AMMO, weaponRaycast);
		}
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000BA23 File Offset: 0x00009C23
	private void DoneReload()
	{
		this.isReloading = false;
	}

	// Token: 0x04000218 RID: 536
	public Animator rigController;

	// Token: 0x04000219 RID: 537
	public WeaponAnimationEvent animationEvent;

	// Token: 0x0400021A RID: 538
	public Transform leftHand;

	// Token: 0x0400021B RID: 539
	public ActiveWeapon activeWeapon;

	// Token: 0x0400021C RID: 540
	private GameObject magazineHand;

	// Token: 0x0400021D RID: 541
	public bool isReloading;
}
