
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class AiWeapon : MonoBehaviour
{
	// Token: 0x060000C3 RID: 195 RVA: 0x00005F9B File Offset: 0x0000419B
	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
		this.aiHealth = base.GetComponent<AiHealth>();
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00005FB8 File Offset: 0x000041B8
	public void Attack(Vector3 targetTransform)
	{
		if (this.aiHealth.isHitting)
		{
			return;
		}
		float magnitude = (targetTransform - this.weapon.raycastOrigin.position).magnitude;
		this.inAccurancy = magnitude / 30f;
		Vector3 vector = targetTransform + Random.insideUnitSphere * this.inAccurancy;
		this.timer -= Time.deltaTime;
		Debug.DrawLine(this.weapon.raycastOrigin.position, vector, Color.blue);
		if (this.weapon.ammoCount <= 0)
		{
			this.weapon.StopFiring();
			this.animator.Play("Recharge");
			this.timer = 2f;
			this.weapon.ammoCount = this.weapon.gunInfo[KeyInfo.maxAmmo];
			return;
		}
		if (!this.weapon.isFiring && this.timer < 0f)
		{
			this.weapon.StartFiring();
		}
		this.weapon.UpdateWeapon(Time.deltaTime, vector);
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x000060CC File Offset: 0x000042CC
	public void DropWeapon()
	{
		this.weapon.transform.SetParent(null);
		this.weapon.GetComponent<BoxCollider>().enabled = true;
		this.weapon.GetComponent<BoxCollider>().isTrigger = false;
		this.weapon.AddComponent<Rigidbody>();
		this.weapon.GetComponent<Rigidbody>().useGravity = true;
	}

	// Token: 0x04000114 RID: 276
	public WeaponRaycast weapon;

	// Token: 0x04000115 RID: 277
	public float inAccurancy;

	// Token: 0x04000116 RID: 278
	public float timer;

	// Token: 0x04000117 RID: 279
	private Animator animator;

	// Token: 0x04000118 RID: 280
	private AiHealth aiHealth;
}
