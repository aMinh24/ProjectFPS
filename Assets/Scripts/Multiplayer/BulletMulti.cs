﻿
using UnityEngine;

// Token: 0x0200006C RID: 108
public class BulletMulti : MonoBehaviour
{
	//public GameObject sp;
	//public Vector3 preTF = new Vector3(99,99,99);
	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000AC9B File Offset: 0x00008E9B
	public bool IsActive
	{
		get
		{
			return this.isActive;
		}
	}
    //private void Update()
    //{
		
    //}
    //private void Update()
    //{
    //      if ( preTF.Equals (new Vector3(99, 99, 99)))
    //      {
    //	return;
    //      }
    //if (Vector3.Distance(preTF,transform.position)<=0.1f)
    //{
    //          this.tracer.emitting = false;
    //	sp.SetActive(false);
    //      }
    //else
    //{
    //	this.tracer.emitting = true;
    //	sp.SetActive(true);
    //}
    //preTF = transform.position;
    //}
    // Token: 0x060001F2 RID: 498 RVA: 0x0000ACA4 File Offset: 0x00008EA4
    public void Deactive(Vector3 pos)
	{
		this.isActive = false;
		base.gameObject.SetActive(false);
		this.initialPosition = pos;
		this.initialVelocity = Vector3.zero;
		this.tracer.emitting = false;
		this.tracer.Clear();
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000ACF4 File Offset: 0x00008EF4
	public void Active(Vector3 position, Vector3 velocity)
	{
		this.isActive = true;
		base.gameObject.SetActive(true);
		this.time = 0f;
		this.initialPosition = position;
		this.initialVelocity = velocity;
		this.tracer.emitting = true;
		this.tracer.AddPosition(position);
	}

	// Token: 0x040001DF RID: 479
	public float time;

	// Token: 0x040001E0 RID: 480
	public Vector3 initialPosition;

	// Token: 0x040001E1 RID: 481
	public Vector3 initialVelocity;

	// Token: 0x040001E2 RID: 482
	public TrailRenderer tracer;

	// Token: 0x040001E3 RID: 483
	public bool isActive= true;
}
