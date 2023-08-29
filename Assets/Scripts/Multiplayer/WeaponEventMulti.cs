
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200006F RID: 111
public class WeaponEventMulti : MonoBehaviour
{
	public AudioSource playerAudio;
	// Token: 0x060001FD RID: 509 RVA: 0x0000AFAE File Offset: 0x000091AE
	private void Awake()
	{
		if (weaponEvent != null)
		{
            this.weaponEvent.WeaponAnimEvent.AddListener(new UnityAction<string>(this.OnHolsterEvent));
        }
		
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000AFCC File Offset: 0x000091CC
	private void OnHolsterEvent(string eventName)
	{
		if (eventName == "holster_weapon")
		{
			this.holster_weapon();
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000AFE4 File Offset: 0x000091E4
	private void holster_weapon()
	{
		bool @bool = this.activeWeapon.rigController.GetBool("holster_weapon");
		WeaponRaycastMulti weaponRaycast = this.activeWeapon.GetActiveWeapon();
		MeshRenderer component = weaponRaycast.gameObject.GetComponent<MeshRenderer>();
		int weaponSlot = (int)weaponRaycast.weaponSlot;
		if (@bool)
		{
			if (component != null)
			{
				component.enabled = false;
			}
			weaponRaycast.meshWeapon.SetActive(false);
			this.magMesh[weaponSlot].SetActive(true);
			return;
		}
		if (component != null)
		{
			component.enabled = true;
		}
		weaponRaycast.meshWeapon.SetActive(true);
		this.magMesh[weaponSlot].SetActive(false);
	}

	// Token: 0x040001F1 RID: 497
	public ActiveWeaponMultiplayer activeWeapon;

	// Token: 0x040001F2 RID: 498
	public WeaponAnimationEventMulti weaponEvent;

	// Token: 0x040001F3 RID: 499
	public GameObject[] magMesh;
}
