
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000036 RID: 54
public class AILocomotion : MonoBehaviour
{
	// Token: 0x060000AC RID: 172 RVA: 0x00005823 File Offset: 0x00003A23
	private void Start()
	{
		this.navMeshAgent = base.GetComponent<NavMeshAgent>();
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00005840 File Offset: 0x00003A40
	private void Update()
	{
		if (this.navMeshAgent.hasPath)
		{
			this.animator.SetFloat("Speed", this.navMeshAgent.velocity.magnitude);
			return;
		}
		this.animator.SetFloat("Speed", 0f);
	}

	// Token: 0x04000103 RID: 259
	private NavMeshAgent navMeshAgent;

	// Token: 0x04000104 RID: 260
	private Animator animator;
}
