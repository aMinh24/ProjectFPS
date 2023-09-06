
using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Networking.Types;

// Token: 0x02000052 RID: 82
public class ActiveWeaponMultiplayer : MonoBehaviourPun
{
    public CamMultiplayer camManager;
    public ObjectPoolMulti pool;
    // Token: 0x06000149 RID: 329 RVA: 0x00008300 File Offset: 0x00006500
    private void Awake()
    {
        this.characterAiming = base.GetComponent<CharacterAimingMultiplayer>();
        this.reload = base.GetComponent<WeaponReloadMultiplayer>();
        this.animator = base.GetComponent<Animator>();
        this.weaponEvent = base.GetComponent<WeaponEventMulti>();
        this.characterLocomotion = base.GetComponent<CharacterLocomotionMultiplayer>();
        camManager = GetComponent<CamMultiplayer>();
        this.isHolstered = true;
        //if (!photonView.IsMine) { return; }
        //if (photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
        //{
        //    return;
        //}
        //crossHairTarget = FindObjectOfType<CrossHairTarget>().transform;
        
    }

    // Token: 0x0600014A RID: 330 RVA: 0x00008350 File Offset: 0x00006550
    private void Start()
    {
        this.existWeapon = base.GetComponentsInChildren<WeaponRaycastMulti>();
        for (int i = 0; i < this.existWeapon.Length; i++)
        {
            if (this.existWeapon[i] != null)
            {
                this.Equip(this.existWeapon[i]);
            }
        }
        for (int j = 0; j < this.equippedWeapons.Length; j++)
        {
            if (this.equippedWeapons[j] != null)
            {
                this.SetActiveWeapon(this.equippedWeapons[j].weaponSlot);
                return;
            }
        }
    }

    // Token: 0x0600014B RID: 331 RVA: 0x000083D4 File Offset: 0x000065D4
    private void Update()
    {
        //weapon.UpdateWeapon(Time.deltaTime, this.crossHairTarget.position);
        if (!photonView.IsMine) { return; }
        if (photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }
        if (this.characterAiming.isEcs)
        {
            return;
        }
        WeaponRaycastMulti weapon = GetActiveWeapon(); /*this.GetWeapon(this.activeWeaponIndex);*/
        bool flag = false;
        if (BaseManager<CameraManager>.HasInstance())
        {
            flag = BaseManager<CameraManager>.Instance.isAiming;
        }
        else
        {
            flag = camManager.isAiming;
        }
        if (!this.isChangingWeapon && !this.reload.isReloading && !this.IsFiring() && !this.characterLocomotion.IsSprinting() && !flag)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                this.SetActiveWeapon(ActiveWeaponMultiplayer.WeaponSlot.Primary);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                this.SetActiveWeapon(ActiveWeaponMultiplayer.WeaponSlot.Secondary);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                this.ToggleActiveWeapon();
            }
        }
        bool flag2 = !this.isHolstered && !this.isChangingWeapon && !this.reload.isReloading && !this.characterLocomotion.IsSprinting();
        if (weapon != null && (weapon.weaponName.Equals("scar") || weapon.weaponName.Equals("pistol")))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                this.curShootingMode = weapon.mode[(int)((int)(this.curShootingMode + 1) % (int)(ShootingMode)weapon.mode.Count)];
                if (BaseManager<ListenerManager>.HasInstance())
                {
                    BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_SHOOTING_MODE, this.curShootingMode);
                }
            }
            if (flag2 && this.curShootingMode == ShootingMode.Auto)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    weapon.StartFiring();
                    //photonView.RPC("PlayerStartFiring", RpcTarget.All);
                }
            }
            else if (Input.GetButtonDown("Fire1") && weapon.mode.Contains(ShootingMode.Single) && this.curShootingMode == ShootingMode.Single && flag2)
            {
                weapon.SingleShot(this.crossHairTarget.position);
                //photonView.RPC("PlayerSingleShot", RpcTarget.All);
            }
            weapon.UpdateWeapon(Time.deltaTime, this.crossHairTarget.position);
            //photonView.RPC("PlayerUpdateWeapon", RpcTarget.All);
            if (Input.GetButtonUp("Fire1"))
            {
                weapon.StopFiring();
                //photonView.RPC("PlayerStopFiring", RpcTarget.All);
            }
        }
        //else if (weapon != null && weapon.weaponName.Equals("knife"))
        //{
        //	this.animator.SetLayerWeight(2, 1f);
        //	if (Input.GetButtonDown("Fire1"))
        //	{
        //		this.animator.Play("KnifeAttack");
        //	}
        //}
        //if (this.isHolstered && Input.GetButtonDown("Fire1"))
        //{
        //	this.animator.Play("2HandAttack");
        //}
    }
    [PunRPC]
    public void RecoilWeapon(string name)
    {
        rigController.SetTrigger("recoil_" + name);
        if(GetActiveWeapon().weaponAnimator != null)
        {
            GetActiveWeapon().weaponAnimator.SetTrigger("Fire");

        }
    }
    [PunRPC]
    public void EmitMuzzle()
    {
        GetActiveWeapon().shell.Emit(1);
        foreach (ParticleSystem particleSystem in this.GetWeapon(this.activeWeaponIndex).muzzleFlash)
        {
            particleSystem.Emit(particleSystem.maxParticles);
        }
    }
    //[PunRPC]
    //public void PlayerSingleShot()
    //{
    //       if (!photonView.IsMine) { return; }
    //       if (photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
    //       {
    //           return;
    //       }
    //       GetWeapon(this.activeWeaponIndex).SingleShot(crossHairTarget.gameObject.transform.position);

    //   }
    //[PunRPC]
    //public void PlayerStopFiring()
    //{
    //       if (!photonView.IsMine) { return; }
    //       if (photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
    //       {
    //           return;
    //       }
    //       GetWeapon(this.activeWeaponIndex).StopFiring();

    //   }
    //[PunRPC]
    //public void PlayerStartFiring()
    //{
    //       if (!photonView.IsMine) { return; }
    //       if (photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
    //       {
    //           return;
    //       }
    //       GetWeapon(this.activeWeaponIndex).StartFiring();
    //   }
    //[PunRPC]
    //public void PlayerUpdateWeapon()
    //{
    //       if (!photonView.IsMine) { return; }
    //       if (photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
    //       {
    //           return;
    //       }
    //       this.GetWeapon(this.activeWeaponIndex).UpdateWeapon(Time.deltaTime, this.crossHairTarget.gameObject.transform.position);
    //   }
    // Token: 0x0600014C RID: 332 RVA: 0x0000866C File Offset: 0x0000686C
    public bool IsFiring()
    {
        WeaponRaycastMulti activeWeapon = this.GetActiveWeapon();
        return activeWeapon && activeWeapon.isFiring;
    }

    // Token: 0x0600014D RID: 333 RVA: 0x00008690 File Offset: 0x00006890
    public void Equip(WeaponRaycastMulti newWeapon)
    {
        int weaponSlot = (int)newWeapon.weaponSlot;
        WeaponRaycastMulti weapon = this.GetWeapon(weaponSlot);
        if (weapon)
        {
            UnityEngine.Object.Destroy(weapon.gameObject);
        }
        newWeapon.raycastDestination = this.crossHairTarget;
        newWeapon.weaponRecoil.rigController = this.rigController;
        newWeapon.shooter = base.gameObject.name;
        newWeapon.transform.SetParent(this.weaponSlots[weaponSlot], false);
        this.equippedWeapons[weaponSlot] = newWeapon;
        newWeapon.weaponRecoil.characterAiming = this.characterAiming;
        newWeapon.pool = this.pool;
        if (BaseManager<ListenerManager>.HasInstance())
        {
            BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_AMMO, newWeapon);
            BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_WEAPONUI, this);
            this.curShootingMode = newWeapon.mode[0];
            BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_SHOOTING_MODE, this.curShootingMode);
            BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_TOTAL_AMMO, newWeapon);
            return;
        }
    }

    // Token: 0x0600014E RID: 334 RVA: 0x0000878F File Offset: 0x0000698F
    private WeaponRaycastMulti GetWeapon(int index)
    {
        if (index < 0 || index >= this.equippedWeapons.Length)
        {
            return null;
        }
        return this.equippedWeapons[index];
    }

    // Token: 0x0600014F RID: 335 RVA: 0x000087AA File Offset: 0x000069AA
    public WeaponRaycastMulti GetActiveWeapon()
    {
        //activeWeaponIndex = rigController.GetInteger("weapon_index");
        return this.GetWeapon(this.activeWeaponIndex);
    }

    // Token: 0x06000150 RID: 336 RVA: 0x000087B8 File Offset: 0x000069B8
    private void SetActiveWeapon(ActiveWeaponMultiplayer.WeaponSlot weaponSlot)
    {
        int num = this.activeWeaponIndex;
        if (num == (int)weaponSlot)
        {
            num = -1;
        }
        base.StartCoroutine(this.SwitchWeapon(num, (int)weaponSlot));
    }

    // Token: 0x06000151 RID: 337 RVA: 0x000087E3 File Offset: 0x000069E3
    private IEnumerator SwitchWeapon(int holsterIndex, int activeIndex)
    {
        this.rigController.SetInteger("weapon_index", activeIndex);
        yield return base.StartCoroutine(this.HolsterWeapon(holsterIndex));
        yield return base.StartCoroutine(this.ActivateWeapon(activeIndex));
        this.activeWeaponIndex = activeIndex;
        yield break;
    }

    // Token: 0x06000152 RID: 338 RVA: 0x00008800 File Offset: 0x00006A00
    private void ToggleActiveWeapon()
    {
        if (this.rigController.GetBool("holster_weapon"))
        {
            base.StartCoroutine(this.ActivateWeapon(this.activeWeaponIndex));
            return;
        }
        base.StartCoroutine(this.HolsterWeapon(this.activeWeaponIndex));
    }

    // Token: 0x06000153 RID: 339 RVA: 0x0000883B File Offset: 0x00006A3B
    private IEnumerator HolsterWeapon(int index)
    {
        this.isChangingWeapon = true;
        if (this.GetWeapon(index))
        {
            this.rigController.SetBool("holster_weapon", true);
            yield return new WaitForSeconds(0.1f);
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while ((double)this.rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0);
        }
        this.isHolstered = true;
        this.isChangingWeapon = false;
        if (BaseManager<ListenerManager>.HasInstance())
        {
            BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_AMMO, this);
            BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_WEAPONUI, this);
        }
        yield break;
    }

    // Token: 0x06000154 RID: 340 RVA: 0x00008851 File Offset: 0x00006A51
    private IEnumerator ActivateWeapon(int index)
    {
        this.isChangingWeapon = true;
        WeaponRaycastMulti weapon = this.GetWeapon(index);
        if (weapon)
        {
            this.activeWeaponIndex = index;
            this.rigController.SetBool("holster_weapon", false);
            this.rigController.Play("equip_" + weapon.weaponName);
            yield return new WaitForSeconds(0.1f);
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while ((double)this.rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0);
            this.isHolstered = false;
            if (BaseManager<ListenerManager>.HasInstance())
            {
                BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_AMMO, weapon);
                BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_WEAPONUI, this);
                this.curShootingMode = weapon.mode[0];
                BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_SHOOTING_MODE, this.curShootingMode);
                BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_TOTAL_AMMO, weapon);
            }
        }
        this.curShootingMode = weapon.mode[0];
        if (BaseManager<ListenerManager>.HasInstance())
        {
            BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_SHOOTING_MODE, this.curShootingMode);
        }
        this.isHolstered = false;
        this.isChangingWeapon = false;
        yield break;
    }

    // Token: 0x06000155 RID: 341 RVA: 0x00008868 File Offset: 0x00006A68
    public void DropWeapon()
    {
        WeaponRaycastMulti activeWeapon = this.GetActiveWeapon();
        if (activeWeapon)
        {
            activeWeapon.transform.SetParent(null);
            activeWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            activeWeapon.gameObject.AddComponent<Rigidbody>();
            this.equippedWeapons[this.activeWeaponIndex] = null;
        }
        if (BaseManager<ListenerManager>.HasInstance())
        {
            BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_AMMO, this);
        }
    }

    // Token: 0x0400016F RID: 367
    public WeaponRaycastMulti[] equippedWeapons = new WeaponRaycastMulti[4];

    // Token: 0x04000170 RID: 368
    public int activeWeaponIndex;

    // Token: 0x04000171 RID: 369
    public Animator rigController;

    // Token: 0x04000172 RID: 370
    public Transform[] weaponSlots;

    // Token: 0x04000173 RID: 371
    public Transform crossHairTarget;

    // Token: 0x04000174 RID: 372
    private Animator animator;

    // Token: 0x04000175 RID: 373
    public bool isChangingWeapon;

    // Token: 0x04000176 RID: 374
    public bool isHolstered;

    // Token: 0x04000177 RID: 375
    public CharacterAimingMultiplayer characterAiming;

    // Token: 0x04000178 RID: 376
    public WeaponReloadMultiplayer reload;

    // Token: 0x04000179 RID: 377
    private WeaponEventMulti weaponEvent;

    // Token: 0x0400017A RID: 378
    public WeaponRaycastMulti[] existWeapon;

    // Token: 0x0400017B RID: 379
    private CharacterLocomotionMultiplayer characterLocomotion;

    // Token: 0x0400017C RID: 380
    private ShootingMode curShootingMode;

    // Token: 0x020001FE RID: 510
    public enum WeaponSlot
    {
        // Token: 0x04000A59 RID: 2649
        Primary,
        // Token: 0x04000A5A RID: 2650
        Secondary,
        // Token: 0x04000A5B RID: 2651
        Knife
    }
}
