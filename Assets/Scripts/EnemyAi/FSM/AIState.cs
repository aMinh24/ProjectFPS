

// Token: 0x0200003F RID: 63
public interface AIState
{
	// Token: 0x060000CE RID: 206
	AIStateID GetID();

	// Token: 0x060000CF RID: 207
	void Enter(AiAgent agent);

	// Token: 0x060000D0 RID: 208
	void Update(AiAgent agent);

	// Token: 0x060000D1 RID: 209
	void Exit(AiAgent agent);
}
