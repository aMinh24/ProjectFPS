
using UnityEngine;

// Token: 0x0200003D RID: 61
public class AiWeaponIK : MonoBehaviour
{
	// Token: 0x060000C8 RID: 200 RVA: 0x00006144 File Offset: 0x00004344
	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00006154 File Offset: 0x00004354
	public void AimTarget(Vector3 targetTransform)
	{
		Vector3 vector = targetTransform - (base.transform.position + new Vector3(0f, 2.2f, 0f));
		float num = Vector3.Angle(base.transform.forward, vector);
		if (vector.y < 0f)
		{
			num = -num;
		}
		this.animator.SetFloat("Rotation", num / 10f);
		vector.y = 0f;
		Quaternion rotation = Quaternion.LookRotation(vector);
		base.transform.rotation = rotation;
	}

	// Token: 0x0400011B RID: 283
	public Transform aimOrigin;

	// Token: 0x0400011C RID: 284

	// Token: 0x0400011D RID: 285
	public Vector3 targetOffset;

	// Token: 0x0400011E RID: 286
	public float angleLimit;

	// Token: 0x0400011F RID: 287
	public float distanceLimit;

	// Token: 0x04000120 RID: 288
	private Animator animator;

	// Token: 0x04000121 RID: 289
	public Transform[] boneTransform;

	// Token: 0x04000122 RID: 290
	public float rotationSpeed = 10f;

	// Token: 0x04000123 RID: 291
	public float Angle;
}
