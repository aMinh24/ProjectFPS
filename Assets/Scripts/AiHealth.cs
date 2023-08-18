
using System.Collections;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class AiHealth : Health
{
	// Token: 0x060000A5 RID: 165 RVA: 0x00005683 File Offset: 0x00003883
	public void OnEndHitAnim()
	{
		this.aiAgent.navMeshAgent.enabled = true;
		this.isHitting = false;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x0000569D File Offset: 0x0000389D
	protected override void OnDamage(Vector3 direction, Rigidbody rigidbody)
	{
		this.aiAgent.navMeshAgent.enabled = false;
		this.isHitting = true;
		this.aiAgent.animator.Play("Gethit");
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x000056CC File Offset: 0x000038CC
	protected override void OnDeath(Vector3 direction, Rigidbody rigidbody)
	{
		AIStateDeath aistateDeath = this.aiAgent.stateMachine.GetState(AIStateID.Death) as AIStateDeath;
		aistateDeath.direction = direction;
		aistateDeath.rigidbody = rigidbody;
		if (BaseManager<ListenerManager>.HasInstance())
		{
			string[] value = new string[]
			{
				this.shooter,
				base.gameObject.name
			};
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.ON_ALLY_KILL, value);
		}
		this.aiAgent.stateMachine.ChangeState(AIStateID.Death);
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000573F File Offset: 0x0000393F
	public IEnumerator OnDestroyWhenDeath()
	{
		yield return new WaitForSeconds(this.timeDestroyAI);
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00005750 File Offset: 0x00003950
	protected override void OnStart()
	{
		this.aiAgent = base.GetComponent<AiAgent>();
		if (BaseManager<DataManager>.HasInstance())
		{
			this.maxHealth = BaseManager<DataManager>.Instance.GlobalConfig.maxHealth;
			this.timeDestroyAI = BaseManager<DataManager>.Instance.GlobalConfig.timeDestroyAI;
		}
		this.currentHealth = this.maxHealth;
		this.SetUp();
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000057AC File Offset: 0x000039AC
	private void SetUp()
	{
		foreach (Rigidbody rigidbody in base.GetComponentsInChildren<Rigidbody>())
		{
			rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			HitBox hitBox = rigidbody.gameObject.AddComponent<HitBox>();
			hitBox.health = this;
			hitBox.rb = rigidbody;
			if (hitBox.gameObject != base.gameObject)
			{
				hitBox.gameObject.layer = LayerMask.NameToLayer("HitboxEnemy");
			}
		}
	}

	// Token: 0x04000100 RID: 256
	private float timeDestroyAI;

	// Token: 0x04000101 RID: 257
	private AiAgent aiAgent;

	// Token: 0x04000102 RID: 258
	public bool isHitting;
}
