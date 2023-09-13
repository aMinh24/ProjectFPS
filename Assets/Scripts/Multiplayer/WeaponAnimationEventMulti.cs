
using UnityEngine;
using Photon.Pun;
// Token: 0x02000049 RID: 73
public class WeaponAnimationEventMulti : MonoBehaviourPun
{
	public AudioSource playerAudio;
	public AudioSource weaponAudio;
	// Token: 0x060000F2 RID: 242 RVA: 0x000066AB File Offset: 0x000048AB
	public void OnAnimationEvent(string eventName)
	{
		if (!photonView.IsMine || photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber) { return; }
		this.WeaponAnimEvent.Invoke(eventName);
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x000066B9 File Offset: 0x000048B9
	public void OnHolsterEvent(string eventName)
	{
        //if (!photonView.IsMine || photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber) { return; }
        this.WeaponAnimEvent.Invoke(eventName);
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x000066C7 File Offset: 0x000048C7
	public void OnAudioEvent(string audioName)
	{
		if (audioName.Equals("walk"))
		{
			audioName += Random.Range(0, 5);
            if (BaseManager<AudioManager>.HasInstance())
            {
                playerAudio.PlayOneShot(BaseManager<AudioManager>.Instance.GetAudioClip(audioName));
            }
        }
		else
		{
            weaponAudio.PlayOneShot(BaseManager<AudioManager>.Instance.GetAudioClip(audioName));
        }
	}

	// Token: 0x04000134 RID: 308
	public _AnimationEvent WeaponAnimEvent = new _AnimationEvent();
}
