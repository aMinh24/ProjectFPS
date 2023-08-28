
using Photon.Pun;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class RagdollMulti : MonoBehaviour
{
	// Token: 0x060000E4 RID: 228 RVA: 0x000064B9 File Offset: 0x000046B9
	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
		this.rigidbodies = base.GetComponentsInChildren<Rigidbody>();
		this.DeactiveRagdoll();
	}
    private void Update()
    {
        PhotonView view = this.gameObject.GetComponent<PhotonView>();
		if (view.IsMine&& view.CreatorActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
		{
			if (Input.GetKeyDown(KeyCode.K))
			{
				ActiveRagdoll();
			}
		}
    }
    // Token: 0x060000E5 RID: 229 RVA: 0x000064DC File Offset: 0x000046DC
    public void ActiveRagdoll()
	{
		this.animator.enabled = false;
		Rigidbody[] array = this.rigidbodies;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].isKinematic = false;
		}
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00006514 File Offset: 0x00004714
	public void DeactiveRagdoll()
	{
		this.animator.enabled = true;
		Rigidbody[] array = this.rigidbodies;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].isKinematic = true;
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x0000654B File Offset: 0x0000474B
	public void ApplyForce(Vector3 force, Rigidbody rigidbody)
	{
		rigidbody.AddForce(force, ForceMode.VelocityChange);
	}

	// Token: 0x04000131 RID: 305
	private Animator animator;

	// Token: 0x04000132 RID: 306
	private Rigidbody[] rigidbodies;
}
