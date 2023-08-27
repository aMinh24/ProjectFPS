
using UnityEngine;
using Photon.Pun;
// Token: 0x02000043 RID: 67
public class HitBoxMulti : MonoBehaviourPun
{
	// Token: 0x060000E2 RID: 226 RVA: 0x00006458 File Offset: 0x00004658
	public void OnHit(WeaponRaycastMulti weapon, Vector3 direction)
	{
		float num = (float)weapon.gunInfo[KeyInfo.damage];
		if (base.tag.Equals("Head"))
		{
			num *= 2f;
		}
		photonView.RPC("RPCTakeDame", RpcTarget.All, weapon.shooter, num, direction, this.idRb, photonView.ViewID);
	}
	
	public PhotonView photonView;
	public int idRb;
	// Token: 0x0400012F RID: 303
	public HealthMultiplayer health;

	// Token: 0x04000130 RID: 304
	public Rigidbody rb;
}
