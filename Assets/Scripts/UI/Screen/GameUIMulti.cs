using Photon.Pun;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000067 RID: 103
public class GameUIMulti : BaseScreen
{
	// Token: 0x060001CF RID: 463 RVA: 0x0000A36B File Offset: 0x0000856B
	public override void Hide()
	{
		base.Hide();
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000A374 File Offset: 0x00008574
	private void OnDestroy()
	{
		if (BaseManager<ListenerManager>.HasInstance())
		{
			BaseManager<ListenerManager>.Instance.Unregister(ListenType.UPDATE_AMMO, new Action<object>(this.UpdateAmmo));
			BaseManager<ListenerManager>.Instance.Unregister(ListenType.UPDATE_HEALTH, new Action<object>(this.UpdateHealth));
			BaseManager<ListenerManager>.Instance.Unregister(ListenType.CHANGE_SCOPE, new Action<object>(this.changeScope));
			BaseManager<ListenerManager>.Instance.Unregister(ListenType.UPDATE_WEAPONUI, new Action<object>(this.UpdateActiveWeapon));
			BaseManager<ListenerManager>.Instance.Unregister(ListenType.UPDATE_TOTAL_AMMO, new Action<object>(this.UpdateAmmoTotal));
			BaseManager<ListenerManager>.Instance.Unregister(ListenType.UPDATE_SHOOTING_MODE, new Action<object>(this.changeShootingMode));
			BaseManager<ListenerManager>.Instance.Unregister(ListenType.ON_ENEMY_KILL, new Action<object>(this.UpdateEnemyKill));
			BaseManager<ListenerManager>.Instance.Unregister(ListenType.ON_ALLY_KILL, new Action<object>(this.UpdateAllyKill));
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.ON_END_MISSION, null);
		}
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000A454 File Offset: 0x00008654
	public override void Init()
	{
		base.Init();
		this.crossHairUI.SetActive(true);
		this.scopeUI.SetActive(false);
		if (BaseManager<ListenerManager>.HasInstance())
		{
			BaseManager<ListenerManager>.Instance.Register(ListenType.UPDATE_AMMO, new Action<object>(this.UpdateAmmo));
			BaseManager<ListenerManager>.Instance.Register(ListenType.UPDATE_HEALTH, new Action<object>(this.UpdateHealth));
			BaseManager<ListenerManager>.Instance.Register(ListenType.CHANGE_SCOPE, new Action<object>(this.changeScope));
			BaseManager<ListenerManager>.Instance.Register(ListenType.UPDATE_ENERGY, new Action<object>(this.UpdateEnergyBar));
			BaseManager<ListenerManager>.Instance.Register(ListenType.UPDATE_WEAPONUI, new Action<object>(this.UpdateActiveWeapon));
			BaseManager<ListenerManager>.Instance.Register(ListenType.UPDATE_TOTAL_AMMO, new Action<object>(this.UpdateAmmoTotal));
			BaseManager<ListenerManager>.Instance.Register(ListenType.UPDATE_SHOOTING_MODE, new Action<object>(this.changeShootingMode));
			BaseManager<ListenerManager>.Instance.Register(ListenType.ON_ENEMY_KILL, new Action<object>(this.UpdateEnemyKill));
			BaseManager<ListenerManager>.Instance.Register(ListenType.ON_ALLY_KILL, new Action<object>(this.UpdateAllyKill));
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.ON_START_MISSION, null);
		}
		if (BaseManager<DataManager>.HasInstance())
		{
			this.totalEnemyText.text = "/" + BaseManager<DataManager>.Instance.GlobalConfig.totalEnemy.ToString();
			this.timeRemaining = BaseManager<DataManager>.Instance.GlobalConfig.maxTimePlay * 60f;
		}
		for (int i = 0; i < this.rowKill.Length; i++)
		{
			this.rowKill[i].orderNum = i;
			this.rowKill[i].gameObject.SetActive(false);
		}
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000A5EC File Offset: 0x000087EC
	private void Update()
	{
		this.timeRemainingText.text = string.Format("{0}:{1}", math.floor(this.timeRemaining / 60f), math.floor(this.timeRemaining - math.floor(this.timeRemaining / 60f) * 60f));
		this.timeRemaining -= Time.deltaTime;		
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000A65E File Offset: 0x0000885E
	public override void Show(object data)
	{
		base.Show(data);
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000A668 File Offset: 0x00008868
	public void UpdateEnemyKill(object data)
	{
		string[] array = data as string[];
		if (array != null)
		{
			this.rowKill[this.curRow].OnEnemyKill(array[0], array[1]);
			this.rowKill[this.curRow].gameObject.transform.SetAsFirstSibling();
			if (!this.rowKill[this.curRow].gameObject.active)
			{
				this.rowKill[this.curRow].gameObject.SetActive(true);
				base.StartCoroutine(this.UnactiveRow(this.curRow));
			}
			this.curRow = (this.curRow + 1) % this.rowKill.Length;
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000A714 File Offset: 0x00008914
	public void UpdateAllyKill(object data)
	{
		string[] array = data as string[];
		if (array != null)
		{
			this.rowKill[this.curRow].OnAllyKill(array[0], array[1]);
			this.rowKill[this.curRow].gameObject.transform.SetAsFirstSibling();
			if (!this.rowKill[this.curRow].gameObject.active)
			{
				this.rowKill[this.curRow].gameObject.SetActive(true);
				base.StartCoroutine(this.UnactiveRow(this.curRow));
			}
			this.curRow = (this.curRow + 1) % this.rowKill.Length;
		}
		this.currentEnemyText.text = (int.Parse(this.currentEnemyText.text) + 1).ToString();
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0000A80F File Offset: 0x00008A0F
	private IEnumerator UnactiveRow(int n)
	{
		yield return new WaitForSeconds(60f);
		this.rowKill[n].gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0000A828 File Offset: 0x00008A28
	public void UpdateEnergyBar(object data)
	{
		if (data is float)
		{
			float value = (float)data;
			this.energyBar.value = value;
		}
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000A850 File Offset: 0x00008A50
	public void changeShootingMode(object data)
	{
		if (data is ShootingMode)
		{
			ShootingMode shootingMode = (ShootingMode)data;
			GameObject[] array = this.modeIcon;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
			this.modeIcon[(int)shootingMode].SetActive(true);
			this.modeText.text = shootingMode.ToString();
		}
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000A8B0 File Offset: 0x00008AB0
	public void changeScope(object data = null)
	{
		if (data is bool && (bool)data)
		{
			this.scopeUI.SetActive(false);
			this.crossHairUI.SetActive(true);
			return;
		}
		if (!this.scopeUI.active)
		{
			this.scopeUI.SetActive(true);
			this.crossHairUI.SetActive(false);
			return;
		}
		this.scopeUI.SetActive(false);
		this.crossHairUI.SetActive(true);
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000A924 File Offset: 0x00008B24
	public void UpdateHealth(object value)
	{
		PlayerHealth playerHealth = value as PlayerHealth;
		if (playerHealth != null)
		{
			this.healthBar.value = playerHealth.currentHealth * 1f / 100f;
			this.healthText.text = playerHealth.currentHealth.ToString() + "%";
		}
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000A978 File Offset: 0x00008B78
	public void UpdateAmmo(object value)
	{
        WeaponRaycastMulti weaponRaycastMulti = value as WeaponRaycastMulti;
        //if (!weaponRaycastMulti.photonView.IsMine) { return; }
        //if (weaponRaycastMulti.photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
        //{
        //    return;
        //}
        if (weaponRaycastMulti != null && weaponRaycastMulti.equipBy == EquipBy.Player)
        {
            this.ammoText.text = weaponRaycastMulti.ammoCount.ToString();
        }
        if (value == null)
        {
            this.ammoText.text = "0";
        }
    }

	// Token: 0x060001DC RID: 476 RVA: 0x0000A9C0 File Offset: 0x00008BC0
	public void UpdateAmmoTotal(object value)
	{
        WeaponRaycastMulti weaponRaycastMulti = value as WeaponRaycastMulti;
        //if (!weaponRaycastMulti.photonView.IsMine) { return; }
        //if (weaponRaycastMulti.photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
        //{
        //    return;
        //}
        if (weaponRaycastMulti != null && weaponRaycastMulti.equipBy == EquipBy.Player)
        {
            this.ammoTotalText.text = weaponRaycastMulti.totalAmmo.ToString();
        }
        if (value == null)
        {
            this.ammoText.text = "0";
        }

    }

	// Token: 0x060001DD RID: 477 RVA: 0x0000AA00 File Offset: 0x00008C00
	public void UpdateActiveWeapon(object? value)
	{
        WeaponRaycastMulti weaponRaycast = value as WeaponRaycastMulti;
			//if (!weaponRaycast.photonView.IsMine) { return; }
			//if (weaponRaycast.photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
			//{
			//    return;
			//}
        GameObject[] activeWeaponOverlay = this.ActiveWeaponOverlay;
		for (int i = 0; i < activeWeaponOverlay.Length; i++)
		{
			activeWeaponOverlay[i].SetActive(false);
		}
		if (weaponRaycast != null)
		{
			this.ActiveWeaponOverlay[(int)weaponRaycast.weaponSlot].SetActive(true);
			this.weaponName.text = weaponRaycast.weaponName.ToString().ToUpper();
			return;
		}
		this.weaponName.text = "FIST";
	}

	// Token: 0x040001C6 RID: 454
	public Text ammoText;

	// Token: 0x040001C7 RID: 455
	public Text ammoTotalText;

	// Token: 0x040001C8 RID: 456
	public Slider healthBar;

	// Token: 0x040001C9 RID: 457
	public Text healthText;

	// Token: 0x040001CA RID: 458
	public Text weaponName;

	// Token: 0x040001CB RID: 459
	public GameObject scopeUI;

	// Token: 0x040001CC RID: 460
	public GameObject crossHairUI;

	// Token: 0x040001CD RID: 461
	public Slider energyBar;

	// Token: 0x040001CE RID: 462
	public GameObject[] ActiveWeaponOverlay;

	// Token: 0x040001CF RID: 463
	public GameObject[] modeIcon;

	// Token: 0x040001D0 RID: 464
	public Text modeText;

	// Token: 0x040001D1 RID: 465
	public Text totalEnemyText;

	// Token: 0x040001D2 RID: 466
	public Text currentEnemyText;

	// Token: 0x040001D3 RID: 467
	public Text timeRemainingText;

	// Token: 0x040001D4 RID: 468
	public float timeRemaining;

	// Token: 0x040001D5 RID: 469
	public KillDisplay[] rowKill;

	// Token: 0x040001D6 RID: 470
	public int curRow;
}
