
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class PlayerHealthMultiplayer : HealthMultiplayer
{
	public Rigidbody[] m_Rigidbodies;
	// Token: 0x06000179 RID: 377 RVA: 0x00009582 File Offset: 0x00007782
	protected override void OnDamage(Vector3 direction, int idRb)
	{
		
		if (BaseManager<ListenerManager>.HasInstance())
		{
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_HEALTH, this);
		}
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00009598 File Offset: 0x00007798
	protected override void OnDeath(Vector3 direction, int idRb)
	{
		this.ragdoll.ActiveRagdoll();
		
		this.characterController.enabled = false;
		this.characterLocomotion.enabled = false;
		this.ragdoll.ApplyForce(direction, m_Rigidbodies[idRb]);
		if (BaseManager<CameraManager>.HasInstance())
		{
			BaseManager<CameraManager>.Instance.TurnOffScope();
		}
		else
		{
			characterAiming.camManager.TurnOffScope();
		}
        this.characterAiming.enabled = false;

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
		//if (BaseManager<UIManager>.HasInstance())
		//{
		//	BaseManager<UIManager>.Instance.ShowScreen<DefeatPanel>(null, true);
		//}
		//yield break;
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
		m_Rigidbodies = GetComponentsInChildren<Rigidbody>();
		PhotonView photonView = GetComponent<PhotonView>();
		for (int i = 0; i < m_Rigidbodies.Length; i++)
		{
			m_Rigidbodies[i].collisionDetectionMode = CollisionDetectionMode.Continuous;
			HitBoxMulti hitBox = m_Rigidbodies[i].gameObject.AddComponent<HitBoxMulti>();
			hitBox.idRb = i;
			hitBox.photonView = photonView;
			hitBox.health = this;
			hitBox.rb = m_Rigidbodies[i];
			PhotonView view = GetComponent<PhotonView>();
			string layerHitbox = "HitboxEnemy";
			if (view.IsMine && view.CreatorActorNr == PhotonNetwork.LocalPlayer.ActorNumber) layerHitbox = "HitboxPlayer";
            if (hitBox.gameObject != base.gameObject)
			{
				hitBox.gameObject.layer = LayerMask.NameToLayer(layerHitbox);
			}
		}
	}
	
	// Token: 0x040001AE RID: 430
	public Ragdoll ragdoll;

	// Token: 0x040001AF RID: 431
	public ActiveWeaponMultiplayer activeWeapon;

	// Token: 0x040001B0 RID: 432
	public CharacterAimingMultiplayer characterAiming;

	// Token: 0x040001B1 RID: 433
	public CharacterLocomotionMultiplayer characterLocomotion;

	// Token: 0x040001B2 RID: 434
	public CharacterController characterController;

	// Token: 0x040001B3 RID: 435
	public Transform spine;
}
