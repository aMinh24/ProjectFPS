
using UnityEngine;

// Token: 0x0200006D RID: 109
public class CrossHairTarget : MonoBehaviour
{
	// Token: 0x060001F5 RID: 501 RVA: 0x0000AD4D File Offset: 0x00008F4D
	private void Awake()
	{
		this.mainCamera = Camera.main;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0000AD5C File Offset: 0x00008F5C
	private void FixedUpdate()
	{
		this.ray.origin = this.mainCamera.transform.position;
		this.ray.direction = this.mainCamera.transform.forward;
		if (Physics.Raycast(this.ray, out this.hitInfo))
		{
			base.transform.position = this.hitInfo.point;
			return;
		}
		base.transform.position = this.ray.GetPoint(500f);
	}

	// Token: 0x040001E4 RID: 484
	public Camera mainCamera;

	// Token: 0x040001E5 RID: 485
	private Ray ray;

	// Token: 0x040001E6 RID: 486
	private RaycastHit hitInfo;
}
