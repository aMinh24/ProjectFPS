
using UnityEngine;

// Token: 0x02000042 RID: 66
public class HealthMultiplayer : MonoBehaviour
{
	// Token: 0x060000D9 RID: 217 RVA: 0x000063C3 File Offset: 0x000045C3
	private void Start()
	{
		this.OnStart();
	}

	// Token: 0x060000DA RID: 218 RVA: 0x000063CC File Offset: 0x000045CC
	public void TakeDamage(float amount, Vector3 direction, int idRb)
	{
		if (this.isDead)
		{
			return;
		}
		this.currentHealth -= amount;
		Debug.Log("cur " + currentHealth);
		if (this.currentHealth <= 0f)
		{
			this.currentHealth = 0f;
		}
		this.OnDamage(direction, idRb);
		if (this.currentHealth <= 0f)
		{
			this.isDead = true;
			this.Die(direction, idRb);
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000642C File Offset: 0x0000462C
	public bool IsDead()
	{
		return this.currentHealth <= 0f;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x0000643E File Offset: 0x0000463E
	private void Die(Vector3 direction, int idRb)
	{
		this.OnDeath(direction, idRb);
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00006448 File Offset: 0x00004648
	protected virtual void OnStart()
	{
	}

	// Token: 0x060000DE RID: 222 RVA: 0x0000644A File Offset: 0x0000464A
	protected virtual void OnDeath(Vector3 direction, int idRb)
	{
	}

	// Token: 0x060000DF RID: 223 RVA: 0x0000644C File Offset: 0x0000464C
	protected virtual void OnDamage(Vector3 direction, int idRb)
	{
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x0000644E File Offset: 0x0000464E
	protected virtual void OnHealth(float amount)
	{
	}

	// Token: 0x0400012B RID: 299
	protected float maxHealth;

	// Token: 0x0400012C RID: 300
	public float currentHealth;

	// Token: 0x0400012D RID: 301
	public string shooter;

	// Token: 0x0400012E RID: 302
	public bool isDead;
}
