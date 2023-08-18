using System;


// Token: 0x02000040 RID: 64
public class AiStateMachine
{
	// Token: 0x060000D2 RID: 210 RVA: 0x00006204 File Offset: 0x00004404
	public AiStateMachine(AiAgent agent)
	{
		this.agent = agent;
		int num = Enum.GetNames(typeof(AIStateID)).Length;
		this.states = new AIState[num];
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x0000623C File Offset: 0x0000443C
	public void RegisterState(AIState state)
	{
		int id = (int)state.GetID();
		this.states[id] = state;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x0000625C File Offset: 0x0000445C
	public AIState GetState(AIStateID stateID)
	{
		return this.states[(int)stateID];
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00006273 File Offset: 0x00004473
	public void Update()
	{
		AIState state = this.GetState(this.currentState);
		if (state == null)
		{
			return;
		}
		state.Update(this.agent);
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00006294 File Offset: 0x00004494
	public void ChangeState(AIStateID newState)
	{
		AIState state = this.GetState(this.currentState);
		if (state != null)
		{
			state.Exit(this.agent);
		}
		this.currentState = newState;
		AIState state2 = this.GetState(this.currentState);
		if (state2 == null)
		{
			return;
		}
		state2.Enter(this.agent);
	}

	// Token: 0x04000124 RID: 292
	public AIState[] states;

	// Token: 0x04000125 RID: 293
	public AIStateID currentState;

	// Token: 0x04000126 RID: 294
	public AiAgent agent;
}
