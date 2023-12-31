﻿using System;
using System.Runtime.CompilerServices;
using Cinemachine;
using UnityEngine;
using UnityEngine.Animations;

// Token: 0x02000053 RID: 83
public class CharacterAiming : MonoBehaviour
{


	private void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		this.activeWeapon = base.GetComponent<ActiveWeapon>();
		if (BaseManager<ListenerManager>.HasInstance())
		{
			BaseManager<ListenerManager>.Instance.Register(ListenType.CHANGE_SCOPE, new Action<object>(this.ChangeCamInfo));
		}
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00008928 File Offset: 0x00006B28
	private void Update()
	{
		if(activeWeapon.GetActiveWeapon() != null)
		{
            if (this.activeWeapon.GetActiveWeapon().weaponSlot == ActiveWeapon.WeaponSlot.Primary && Input.GetKeyDown(KeyCode.Mouse1))
            {
				if(CameraManager.HasInstance())
				{
                    BaseManager<CameraManager>.Instance.ChangeScope();
                }
            }
        }
		
		if (Input.GetKeyDown(KeyCode.Escape) && !this.isEcs)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			if (BaseManager<UIManager>.HasInstance())
			{
				BaseManager<UIManager>.Instance.ShowPopup<GameMenu>(null, false);
				this.isEcs = true;
			}
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (!Cursor.visible)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				return;
			}
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000089C4 File Offset: 0x00006BC4
	private void FixedUpdate()
	{
		if (!this.isEcs)
		{
			this.moveAxist();
		}
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000089D4 File Offset: 0x00006BD4
	private void moveAxist()
	{
		this.xAxist.Update(Time.fixedDeltaTime);
		this.yAxist.Update(Time.fixedDeltaTime);
		this.cameraLookAt[this.curLookAt].eulerAngles = new Vector3(this.yAxist.Value, this.xAxist.Value, 0f);
		float y = this.mainCamera.transform.rotation.eulerAngles.y;
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, y, 0f), this.turnSpeed * Time.fixedDeltaTime);
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00008A8C File Offset: 0x00006C8C
	public void ChangeCamInfo(object data)
	{
		if (BaseManager<AudioManager>.HasInstance())
		{
			BaseManager<AudioManager>.Instance.PlaySE("ChangeScope", 0f);
		}
		if (data is bool && (bool)data)
		{
			this.curLookAt = 0;
			this.xAxist.m_MaxSpeed = 300f;
			this.yAxist.m_MaxSpeed = 300f;
			this.xAxist.m_AccelTime = 0.02f;
			this.yAxist.m_AccelTime = 0.02f;
			return;
		}
		bool isAiming = false;
		if (BaseManager<CameraManager>.HasInstance())
		{
			isAiming = BaseManager<CameraManager>.Instance.isAiming;

        }
		if (isAiming)
		{
			this.curLookAt = 0;
			this.xAxist.m_MaxSpeed = 300f;
			this.yAxist.m_MaxSpeed = 300f;
			this.xAxist.m_AccelTime = 0.02f;
			this.yAxist.m_AccelTime = 0.02f;
			return;
		}
		this.curLookAt = 1;
		this.xAxist.m_MaxSpeed = 50f;
		this.yAxist.m_MaxSpeed = 50f;
		this.xAxist.m_AccelTime = 0.1f;
		this.yAxist.m_AccelTime = 0.1f;
	}

	// Token: 0x0400017D RID: 381
	public float turnSpeed = 15f;

	// Token: 0x0400017E RID: 382
	public Camera mainCamera;

	// Token: 0x0400017F RID: 383
	public AxisState xAxist;

	// Token: 0x04000180 RID: 384
	public AxisState yAxist;

	// Token: 0x04000181 RID: 385
	public Transform[] cameraLookAt;

	// Token: 0x04000182 RID: 386
	public ActiveWeapon activeWeapon;

	// Token: 0x04000183 RID: 387
	public bool isEcs;

	// Token: 0x04000184 RID: 388
	public int curLookAt;
}
