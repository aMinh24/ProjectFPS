
using UnityEngine;

// Token: 0x0200002E RID: 46
[CreateAssetMenu]
public class GlobalConfig : ScriptableObject
{
	// Token: 0x040000CE RID: 206
	[Header("AI")]
	public int totalEnemy = 20;

	// Token: 0x040000CF RID: 207
	public float maxTime = 1f;

	// Token: 0x040000D0 RID: 208
	public float maxDistance = 1f;

	// Token: 0x040000D1 RID: 209
	public float maxHealth = 100f;

	// Token: 0x040000D2 RID: 210
	public float blinkDuration = 0.1f;

	// Token: 0x040000D3 RID: 211
	public float dieForce = 10f;

	// Token: 0x040000D4 RID: 212
	public float maxSight = 5f;

	// Token: 0x040000D5 RID: 213
	public float timeDestroyAI = 3f;

	// Token: 0x040000D6 RID: 214
	public float shootingRange = 15f;

	// Token: 0x040000D7 RID: 215
	[Header("Player")]
	public float jumpHeight = 3f;

	// Token: 0x040000D8 RID: 216
	public float gravity = 20f;

	// Token: 0x040000D9 RID: 217
	public float stepDown = 0.4f;

	// Token: 0x040000DA RID: 218
	public float airControl = 2.5f;

	// Token: 0x040000DB RID: 219
	public float jumpDamp = 0.5f;

	// Token: 0x040000DC RID: 220
	public float groundSpeed = 1.2f;

	// Token: 0x040000DD RID: 221
	public float pushPower = 2f;

	// Token: 0x040000DE RID: 222
	public float turnSpeed = 15f;

	// Token: 0x040000DF RID: 223
	public float defaultRecoil = 1f;

	// Token: 0x040000E0 RID: 224
	public float aimRecoil = 0.3f;

	// Token: 0x040000E1 RID: 225
	public float timeDestroyDroppedMagazine = 5f;

	// Token: 0x040000E2 RID: 226
	public float maxCroissHairTargetDistance = 100f;

	// Token: 0x040000E3 RID: 227
	public int maxBulletPoolSize = 30;

	// Token: 0x040000E4 RID: 228
	public float maxEnergy = 10f;

	// Token: 0x040000E5 RID: 229
	public float maxTimePlay = 15f;

	// Token: 0x040000E6 RID: 230
	[Header("UI")]
	public float loadingOverLapTime = 1f;
}
