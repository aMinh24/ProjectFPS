
using UnityEngine;

// Token: 0x02000043 RID: 67
public class HitBox : MonoBehaviour
{
	// Token: 0x060000E2 RID: 226 RVA: 0x00006458 File Offset: 0x00004658
	public void OnHit(WeaponRaycast weapon, Vector3 direction)
	{
		float num = (float)weapon.gunInfo[KeyInfo.damage];
		if (base.tag.Equals("Head"))
		{
			num *= 2f;
		}
		this.health.shooter = weapon.shooter;
		this.health.TakeDamage(num, direction, this.rb);
	}

	// Token: 0x0400012F RID: 303
	public Health health;

	// Token: 0x04000130 RID: 304
	public Rigidbody rb;
}
