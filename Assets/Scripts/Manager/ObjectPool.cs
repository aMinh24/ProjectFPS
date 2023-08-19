
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class ObjectPool : BaseManager<ObjectPool>
{
	// Token: 0x060001F8 RID: 504 RVA: 0x0000ADEC File Offset: 0x00008FEC
	private void Start()
	{
		if (BaseManager<DataManager>.HasInstance())
		{
			this.totalEnemy = BaseManager<DataManager>.Instance.GlobalConfig.totalEnemy;
		}
		this.spawnPos = GameObject.FindGameObjectsWithTag("Respawn").ToList<GameObject>();
		this.poolObjects = new List<Bullet>();
		this.amountToPool = 50;
		for (int i = 0; i < this.amountToPool; i++)
		{
			Bullet bullet = UnityEngine.Object.Instantiate<Bullet>(this.objectToPool, base.transform, true);
			bullet.Deactive();
			this.poolObjects.Add(bullet);
		}
		for (int j = 0; j < this.amountToPool; j++)
		{
			Bullet bullet = UnityEngine.Object.Instantiate<Bullet>(this.objectToPool, base.transform, true);
			bullet.Deactive();
			this.poolAiObjects.Add(bullet);
		}
		//for (int k = 0; k < this.totalEnemy; k++)
		//{
		//	GameObject item = UnityEngine.Object.Instantiate<GameObject>(this.aiToPool, this.spawnPos[k].transform.position, Quaternion.identity);
		//	this.aiPool.Add(item);
		//}
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0000AEF2 File Offset: 0x000090F2
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			base.StartCoroutine(this.aiTest.GetComponent<AiAgent>().EnableAll());
		}
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0000AF14 File Offset: 0x00009114
	public Bullet GetPooledObject()
	{
		for (int i = 0; i < this.amountToPool; i++)
		{
			if (!this.poolObjects[i].IsActive)
			{
				return this.poolObjects[i];
			}
		}
		return null;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0000AF54 File Offset: 0x00009154
	public Bullet GetPooledAiObject()
	{
		for (int i = 0; i < this.amountToPool; i++)
		{
			if (!this.poolAiObjects[i].IsActive)
			{
				return this.poolAiObjects[i];
			}
		}
		return null;
	}

	// Token: 0x040001E7 RID: 487
	[HideInInspector]
	public List<Bullet> poolObjects;

	// Token: 0x040001E8 RID: 488
	public List<Bullet> poolAiObjects;

	// Token: 0x040001E9 RID: 489
	public Bullet objectToPool;

	// Token: 0x040001EA RID: 490
	public List<GameObject> spawnPos;

	// Token: 0x040001EB RID: 491
	public GameObject aiToPool;

	// Token: 0x040001EC RID: 492
	public List<GameObject> aiPool;

	// Token: 0x040001ED RID: 493
	private int totalEnemy;

	// Token: 0x040001EE RID: 494
	private List<GameObject> aiPoolSPawn = new List<GameObject>();

	// Token: 0x040001EF RID: 495
	public GameObject aiTest;

	// Token: 0x040001F0 RID: 496
	private int amountToPool = 100;
}
