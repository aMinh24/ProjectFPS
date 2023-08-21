
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
		//Debug.DrawLine((base.transform.position + new Vector3(0f, 1.3f, 0f)), targetTransform,Color.red);
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







	// Token: 0x04000120 RID: 288
	private Animator animator;


	// Token: 0x04000122 RID: 290
	public float rotationSpeed = 10f;

}
