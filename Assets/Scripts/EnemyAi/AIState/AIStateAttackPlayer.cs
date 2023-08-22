
using UnityEngine;

// Token: 0x02000037 RID: 55
public class AIStateAttackPlayer : AIState
{
	// Token: 0x060000AF RID: 175 RVA: 0x0000589C File Offset: 0x00003A9C
	public void Enter(AiAgent agent)
	{
		if (BaseManager<DataManager>.HasInstance())
		{
			this.maxDistance = BaseManager<DataManager>.Instance.GlobalConfig.maxDistance;
			this.maxTime = BaseManager<DataManager>.Instance.GlobalConfig.maxTime;
			this.shootingRange = BaseManager<DataManager>.Instance.GlobalConfig.shootingRange;
			this.playerHealth = UnityEngine.Object.FindObjectOfType<PlayerHealth>();
		}
		if (agent.navMeshAgent!= null)
		{
            agent.navMeshAgent.speed = 6f;
            agent.navMeshAgent.destination = agent.transform.position;
        }
		
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000592A File Offset: 0x00003B2A
	public void Exit(AiAgent agent)
	{
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x0000592C File Offset: 0x00003B2C
	public AIStateID GetID()
	{
		return AIStateID.AttackPlayer;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00005930 File Offset: 0x00003B30
	public void Update(AiAgent agent)
	{
		if (!agent.navMeshAgent.enabled)
		{
			return;
		}
		Vector3 position = agent.weapon.weapon.raycastOrigin.position;
		Vector3 direction = agent.spinePlayerTransform.position - position;
		Ray ray = new Ray(position, direction);
		float sqrMagnitude = direction.sqrMagnitude;
		if (sqrMagnitude < this.shootingRange * this.shootingRange)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, sqrMagnitude))
			{
				if (raycastHit.collider.gameObject.tag.Equals("Player"))
				{
					agent.weaponIK.AimTarget(agent.spinePlayerTransform.position);
					agent.weapon.Attack(agent.spinePlayerTransform.position);
					agent.navMeshAgent.destination = agent.playerTransform.position;
					agent.navMeshAgent.stoppingDistance = 20f;
					this.timer = 0f;
				}
				else
				{
					agent.navMeshAgent.isStopped = false;
					agent.navMeshAgent.stoppingDistance = 0f;
					if ((agent.navMeshAgent.destination - agent.transform.position).sqrMagnitude < 0.5f)
					{
						this.timer += Time.deltaTime;
					}
				}
				if (this.playerHealth != null && this.playerHealth.IsDead())
				{
					agent.stateMachine.ChangeState(AIStateID.Idle);
				}
			}
		}
		else
		{
			Debug.Log("out range");
			agent.navMeshAgent.stoppingDistance = 0f;
			if ((agent.navMeshAgent.destination - agent.transform.position).sqrMagnitude < 0.5f)
			{
				this.timer += Time.deltaTime;
			}
		}
		if (sqrMagnitude > this.maxDistance * this.maxDistance || this.timer > this.maxTime)
		{
			agent.stateMachine.ChangeState(AIStateID.Idle);
		}
		if (!agent.navMeshAgent.enabled)
		{
			return;
		}
		if (agent.weapon.timer > 0f)
		{
			agent.navMeshAgent.isStopped = true;
			return;
		}
		agent.navMeshAgent.isStopped = false;
	}

	// Token: 0x04000105 RID: 261
	private float timer;

	// Token: 0x04000106 RID: 262
	private float maxDistance;

	// Token: 0x04000107 RID: 263
	private float maxTime;

	// Token: 0x04000108 RID: 264
	private float shootingRange;

	// Token: 0x04000109 RID: 265
	private PlayerHealth playerHealth;
}
