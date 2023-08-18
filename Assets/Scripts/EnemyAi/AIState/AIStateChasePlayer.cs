
using UnityEngine;

// Token: 0x02000038 RID: 56
public class AIStateChasePlayer : AIState
{
	// Token: 0x060000B4 RID: 180 RVA: 0x00005B69 File Offset: 0x00003D69
	public AIStateID GetID()
	{
		return AIStateID.ChasePlayer;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x00005B6C File Offset: 0x00003D6C
	public void Enter(AiAgent agent)
	{
		if (BaseManager<DataManager>.HasInstance())
		{
			this.maxDistance = BaseManager<DataManager>.Instance.GlobalConfig.maxDistance;
			this.maxTime = BaseManager<DataManager>.Instance.GlobalConfig.maxTime;
			this.shootingRange = BaseManager<DataManager>.Instance.GlobalConfig.shootingRange;
		}
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00005BC0 File Offset: 0x00003DC0
	public void Update(AiAgent agent)
	{
		Vector3 vector = agent.transform.position + new Vector3(0f, 2f, 0f);
		Vector3 dir = agent.spinePlayerTransform.position - vector;
		Ray ray = new Ray(vector, dir.normalized);
		Debug.DrawRay(vector, dir, Color.red);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, dir.magnitude))
		{
			if (raycastHit.collider.gameObject.tag.Equals("Player"))
			{
				agent.navMeshAgent.destination = agent.playerTransform.position;
				agent.navMeshAgent.stoppingDistance = 10f;
				this.timer = 0f;
			}
			else
			{
				agent.navMeshAgent.stoppingDistance = 0f;
				if ((agent.navMeshAgent.destination - agent.transform.position).sqrMagnitude < 0.5f)
				{
					this.timer += Time.deltaTime;
				}
			}
		}
		if (dir.sqrMagnitude > this.maxDistance * this.maxDistance || this.timer > this.maxTime)
		{
			agent.stateMachine.ChangeState(AIStateID.Idle);
			return;
		}
		if (dir.sqrMagnitude < this.shootingRange * this.shootingRange)
		{
			agent.stateMachine.ChangeState(AIStateID.AttackPlayer);
			return;
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00005D20 File Offset: 0x00003F20
	public void Exit(AiAgent agent)
	{
	}

	// Token: 0x0400010A RID: 266
	private float timer;

	// Token: 0x0400010B RID: 267
	private float maxDistance;

	// Token: 0x0400010C RID: 268
	private float maxTime;

	// Token: 0x0400010D RID: 269
	private float shootingRange;
}
