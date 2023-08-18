
using UnityEngine;

// Token: 0x02000039 RID: 57
public class AIStateDeath : AIState
{
	// Token: 0x060000B9 RID: 185 RVA: 0x00005D2A File Offset: 0x00003F2A
	public AIStateID GetID()
	{
		return AIStateID.Death;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00005D30 File Offset: 0x00003F30
	public void Enter(AiAgent agent)
	{
		if (BaseManager<DataManager>.HasInstance())
		{
			this.dieForce = BaseManager<DataManager>.Instance.GlobalConfig.dieForce;
		}
		agent.ragdoll.ActiveRagdoll();
		this.direction.y = 1f;
		agent.ragdoll.ApplyForce(this.direction * this.dieForce, this.rigidbody);
		agent.weapon.DropWeapon();
		agent.DisableAll();
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00005DA7 File Offset: 0x00003FA7
	public void Update(AiAgent agent)
	{
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00005DA9 File Offset: 0x00003FA9
	public void Exit(AiAgent agent)
	{
	}

	// Token: 0x0400010E RID: 270
	private float dieForce;

	// Token: 0x0400010F RID: 271
	public Vector3 direction;

	// Token: 0x04000110 RID: 272
	public Rigidbody rigidbody;
}
