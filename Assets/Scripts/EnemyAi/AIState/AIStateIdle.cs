
using UnityEngine;

// Token: 0x0200003A RID: 58
public class AIStateIdle : AIState
{
	// Token: 0x060000BE RID: 190 RVA: 0x00005DB3 File Offset: 0x00003FB3
	public AIStateID GetID()
	{
		return AIStateID.Idle;
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00005DB8 File Offset: 0x00003FB8
	public void Enter(AiAgent agent)
	{
		if (BaseManager<DataManager>.HasInstance())
		{
			this.maxSightDistance = BaseManager<DataManager>.Instance.GlobalConfig.maxSight;
		}
		agent.navMeshAgent.destination = agent.transform.position;
		this.patrolPos = agent.transform.position;
		agent.navMeshAgent.speed = 3f;
		agent.navMeshAgent.isStopped = false;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00005E24 File Offset: 0x00004024
	public void Exit(AiAgent agent)
	{
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00005E28 File Offset: 0x00004028
	public void Update(AiAgent agent)
	{
		agent.animator.SetFloat("Rotation", 0f);
		Vector3 destination = new Vector3(Random.insideUnitCircle.x * 10f + this.patrolPos.x, this.patrolPos.y, Random.insideUnitCircle.y * 10f + this.patrolPos.z);
		if ((agent.transform.position - agent.navMeshAgent.destination).sqrMagnitude < 0.5f)
		{
			agent.navMeshAgent.destination = destination;
		}
		Vector3 position = agent.weapon.weapon.raycastOrigin.position;
		Vector3 position2 = agent.spinePlayerTransform.position;
		this.playerDirection = position2 - position;
		if (this.playerDirection.magnitude > this.maxSightDistance)
		{
			return;
		}
		Vector3 forward = agent.transform.forward;
		float magnitude = this.playerDirection.magnitude;
		Ray ray = new Ray(position, this.playerDirection);
		this.playerDirection.Normalize();
		float num = Vector3.Dot(this.playerDirection, forward);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, magnitude) && num >= 0f && raycastHit.collider.gameObject.tag.Equals("Player"))
		{
			agent.stateMachine.ChangeState(AIStateID.AttackPlayer);
			Debug.Log("ChangeToAttack");
		}
	}

	// Token: 0x04000111 RID: 273
	private Vector3 playerDirection;

	// Token: 0x04000112 RID: 274
	private float maxSightDistance;

	// Token: 0x04000113 RID: 275
	private Vector3 patrolPos;
}
