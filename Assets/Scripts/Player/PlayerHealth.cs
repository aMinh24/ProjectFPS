
using System.Collections;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class PlayerHealth : Health
{
	// Token: 0x06000179 RID: 377 RVA: 0x00009582 File Offset: 0x00007782
	protected override void OnDamage(Vector3 direction, Rigidbody rigidbody)
	{
		
		if (BaseManager<ListenerManager>.HasInstance())
		{
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_HEALTH, this);
		}
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00009598 File Offset: 0x00007798
	protected override void OnDeath(Vector3 direction, Rigidbody rigidbody)
	{
		this.ragdoll.ActiveRagdoll();
		this.characterAiming.enabled = false;
		this.characterController.enabled = false;
		this.characterLocomotion.enabled = false;
		this.ragdoll.ApplyForce(direction, rigidbody);
		if (BaseManager<CameraManager>.HasInstance())
		{
			BaseManager<CameraManager>.Instance.TurnOffScope();
		}
		this.activeWeapon.DropWeapon();
		if (BaseManager<ListenerManager>.HasInstance())
		{
			string[] value = new string[]
			{
				this.shooter,
				base.gameObject.name
			};
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.ON_ENEMY_KILL, value);
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.ON_PLAYER_DEATH, null);
		}
		base.StartCoroutine(this.OnPlayerDeath());
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00009648 File Offset: 0x00007848
	public IEnumerator OnPlayerDeath()
	{
		yield return new WaitForSeconds(2f);
		if (BaseManager<UIManager>.HasInstance())
		{
			BaseManager<UIManager>.Instance.ShowScreen<DefeatPanel>(null, true);
		}
		yield break;
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00009650 File Offset: 0x00007850
	protected override void OnHealth(float amount)
	{
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00009654 File Offset: 0x00007854
	protected override void OnStart()
	{
		if (BaseManager<DataManager>.HasInstance())
		{
			this.maxHealth = BaseManager<DataManager>.Instance.GlobalConfig.maxHealth;
			this.currentHealth = this.maxHealth;
		}
		if (BaseManager<ListenerManager>.HasInstance())
		{
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_HEALTH, this);
		}
		this.SetUp();
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000096A4 File Offset: 0x000078A4
	private void SetUp()
	{
		foreach (Rigidbody rigidbody in base.GetComponentsInChildren<Rigidbody>())
		{
			rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
			HitBox hitBox = rigidbody.gameObject.AddComponent<HitBox>();
			hitBox.health = this;
			hitBox.rb = rigidbody;
			if (hitBox.gameObject != base.gameObject)
			{
				hitBox.gameObject.layer = LayerMask.NameToLayer("HitboxPlayer");
			}
		}
	}

	// Token: 0x040001AE RID: 430
	public Ragdoll ragdoll;

	// Token: 0x040001AF RID: 431
	public ActiveWeapon activeWeapon;

	// Token: 0x040001B0 RID: 432
	public CharacterAiming characterAiming;

	// Token: 0x040001B1 RID: 433
	public CharacterLocomotion characterLocomotion;

	// Token: 0x040001B2 RID: 434
	public CharacterController characterController;

	// Token: 0x040001B3 RID: 435
	public Transform spine;
}
