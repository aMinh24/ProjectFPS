
using Photon.Pun;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class CharacterLocomotionMultiplayer : MonoBehaviourPun
{
	//public PhotonView photonView;
	public CamMultiplayer camManager;
	public AudioSource playerAudio;
	public bool isChatting = false;
	// Token: 0x0600015E RID: 350 RVA: 0x00008BC4 File Offset: 0x00006DC4
	private void Start()
	{
		this.animator = this.GetComponent<Animator>();
		this.characterController = base.GetComponent<CharacterController>();
		this.activeWeapon = base.GetComponent<ActiveWeaponMultiplayer>();
		this.reloadWeapon = base.GetComponent<WeaponReloadMultiplayer>();
		this.jumpHeight = BaseManager<DataManager>.Instance.GlobalConfig.jumpHeight;
		this.gravity = BaseManager<DataManager>.Instance.GlobalConfig.gravity;
		this.stepDown = BaseManager<DataManager>.Instance.GlobalConfig.stepDown;
		this.airControl = BaseManager<DataManager>.Instance.GlobalConfig.airControl;
		this.jumpDamp = BaseManager<DataManager>.Instance.GlobalConfig.jumpDamp;
		this.groundSpeed = BaseManager<DataManager>.Instance.GlobalConfig.groundSpeed;
		this.pushPower = BaseManager<DataManager>.Instance.GlobalConfig.pushPower;
		this.maxEnergy = BaseManager<DataManager>.Instance.GlobalConfig.maxEnergy;
		camManager = GetComponent<CamMultiplayer>();
		this.energy = this.maxEnergy;
		this.isCrouching = false;
		this.checkTheChange = false;
        if (!photonView.IsMine) { return; }
        if (photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }
		if (MultiplayerManager.HasInstance())
		{
			MultiplayerManager.Instance.curCharacterLocomotion = this;
		}
    }

	// Token: 0x0600015F RID: 351 RVA: 0x00008CD0 File Offset: 0x00006ED0
	private void Update()
	{
		if (MultiplayerManager.HasInstance())
		{
			if (!MultiplayerManager.Instance.startTiming) { return; }
		}
        if (!photonView.IsMine) { return; }
        if (photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }
		if(isChatting) { return; }
        this.userInput.x = Input.GetAxis("Horizontal");
		this.userInput.y = Input.GetAxis("Vertical");
		this.animator.SetFloat("InputX", this.userInput.x);
		this.animator.SetFloat("InputY", this.userInput.y);
		this.UpdateIsSprinting();
		if (Input.GetKeyDown(KeyCode.Space) && !this.isCrouching)
		{
			this.Jump();
		}
		if (Input.GetButtonDown("Crouch") && !this.isJumping && !this.IsSprinting())
		{
			this.isCrouching = !this.isCrouching;
		}
		if (this.checkTheChange != this.isCrouching)
		{
			this.Crouch();
		}
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00008D99 File Offset: 0x00006F99
	private void FixedUpdate()
	{
        if (!photonView.IsMine) { return; }
        if (photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }
        if (isChatting) { return; }

        if (this.isJumping)
		{
			bool changing = isJumping;
			this.UpdateInAir();
			if (isJumping!= changing && isJumping == false)
			{
                if (AudioManager.HasInstance())
                {
                    playerAudio.PlayOneShot(AudioManager.Instance.GetAudioClip("Land"));
                }
            }
			return;
		}
		this.UpdateOnGround();
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00008DB0 File Offset: 0x00006FB0
	private void Crouch()
	{
        if (AudioManager.HasInstance())
        {
            playerAudio.PlayOneShot(AudioManager.Instance.GetAudioClip("Crouch"));
        }
        this.checkTheChange = this.isCrouching;
		this.animator.SetBool(this.isCrouchingParam, this.isCrouching);
		if (this.isCrouching)
		{
			characterController.height = 1.2f;
			characterController.center = new Vector3(0, 0.7f, 0);
			this.activeWeapon.GetActiveWeapon().weaponRecoil.recoilModifier = 0.5f;
			return;
		}
        characterController.height = 1.8f;
        characterController.center = new Vector3(0, 1f, 0);
        this.activeWeapon.GetActiveWeapon().weaponRecoil.recoilModifier = 1f;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00008E20 File Offset: 0x00007020
	public bool IsSprinting()
	{
		bool key = Input.GetKey(KeyCode.LeftShift);
		bool flag = this.activeWeapon.IsFiring();
		bool isReloading = this.reloadWeapon.isReloading;
		bool isChangingWeapon = this.activeWeapon.isChangingWeapon;
		bool flag2 = false;
		if (BaseManager<CameraManager>.HasInstance())
		{
			flag2 = BaseManager<CameraManager>.Instance.isAiming;
		}
		else
		{
			flag2 = camManager.isAiming;
		}
		return key && !flag && !isReloading && !isChangingWeapon && this.userInput.y > 0.9f && !flag2 && this.energy > 0f && !this.recover;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00008EA8 File Offset: 0x000070A8
	private void UpdateIsSprinting()
	{
		bool flag = this.IsSprinting();
		if (this.energy <= 0f)
		{
			if(AudioManager.HasInstance())
			{
				playerAudio.PlayOneShot(AudioManager.Instance.GetAudioClip("Breath"));
			}
			this.recover = true;
		}
		if (this.energy > 5f)
		{
			this.recover = false;
		}
		if (flag)
		{
			this.energy -= Time.deltaTime;
		}
		else if (this.userInput.x == 0f && this.userInput.y == 0f)
		{
			this.energy += Time.deltaTime * 2f;
		}
		else
		{
			this.energy += Time.deltaTime;
		}
		if (this.energy >= this.maxEnergy)
		{
			this.energy = this.maxEnergy;
		}
		if (BaseManager<ListenerManager>.HasInstance())
		{
			BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_ENERGY, this.energy / 10f);
		}
		if (flag && this.isCrouching)
		{
			this.isCrouching = false;
		}
		if (!activeWeapon.isHolstered)
		{
			this.rigController.SetBool(this.isSprintingParam, flag);
			this.animator.SetBool(this.isSprintingParam, flag);
		}
		else this.animator.SetBool(isSprintingUnarmParam, flag);

	}

	// Token: 0x06000164 RID: 356 RVA: 0x00008FC0 File Offset: 0x000071C0
	private void UpdateOnGround()
	{
		Vector3 a = this.rootMotion * this.groundSpeed;
		Vector3 b = Vector3.down * this.stepDown;
		this.characterController.Move(a + b);
		this.rootMotion = Vector3.zero;
		if (rigController != null)
		{
			this.rigController.SetBool(this.isJumpingParam, false);
		}

		if (!this.characterController.isGrounded)
		{
			this.SetInAir(0f);
		}
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00009038 File Offset: 0x00007238
	private void UpdateInAir()
	{
		this.velocity.y = this.velocity.y - this.gravity * Time.fixedDeltaTime;
		Vector3 vector = this.velocity * Time.fixedDeltaTime;
		vector += this.CalculateAircontrol();
		this.characterController.Move(vector);
		this.isJumping = !this.characterController.isGrounded;
		this.rootMotion = Vector3.zero;
		this.animator.SetBool("IsJumping", this.isJumping);
	}

	// Token: 0x06000166 RID: 358 RVA: 0x000090C0 File Offset: 0x000072C0
	private void OnAnimatorMove()
	{
        if (!photonView.IsMine) { return; }
        if (photonView.CreatorActorNr != PhotonNetwork.LocalPlayer.ActorNumber)
        {
            return;
        }
        this.rootMotion += this.animator.deltaPosition;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x000090E0 File Offset: 0x000072E0
	private void Jump()
	{
		if (!this.isJumping)
		{
			if (AudioManager.HasInstance())
			{
				playerAudio.PlayOneShot(AudioManager.Instance.GetAudioClip("Jump"));
			}
			float inAir = Mathf.Sqrt(2f * this.gravity * this.jumpHeight);
			this.SetInAir(inAir);
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00009118 File Offset: 0x00007318
	private void SetInAir(float jumpVelocity)
	{
		this.isJumping = true;
		this.velocity = this.animator.velocity * this.jumpDamp * this.groundSpeed;
		this.velocity.y = jumpVelocity;
		this.animator.SetBool(this.isJumpingParam, true);
		if(rigController != null )
		{
            this.rigController.SetBool(this.isJumpingParam, true);
        }
		
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00009184 File Offset: 0x00007384
	private Vector3 CalculateAircontrol()
	{
		return (base.transform.forward * this.userInput.y + base.transform.right * this.userInput.x) * (this.airControl / 100f);
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000091E0 File Offset: 0x000073E0
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
		if (attachedRigidbody == null || attachedRigidbody.isKinematic)
		{
			return;
		}
		if (hit.moveDirection.y < -0.3f)
		{
			return;
		}
		Vector3 a = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);
		attachedRigidbody.velocity = a * this.pushPower;
	}

	// Token: 0x04000185 RID: 389
	public Animator rigController;

	// Token: 0x04000186 RID: 390
	private float jumpHeight;

	// Token: 0x04000187 RID: 391
	private float gravity;

	// Token: 0x04000188 RID: 392
	private float stepDown;

	// Token: 0x04000189 RID: 393
	private float airControl;

	// Token: 0x0400018A RID: 394
	private float jumpDamp;

	// Token: 0x0400018B RID: 395
	private float groundSpeed;

	// Token: 0x0400018C RID: 396
	private float pushPower;

	// Token: 0x0400018D RID: 397
	private Animator animator;

	// Token: 0x0400018E RID: 398
	private CharacterController characterController;

	// Token: 0x0400018F RID: 399
	private ActiveWeaponMultiplayer activeWeapon;

	// Token: 0x04000190 RID: 400
	private WeaponReloadMultiplayer reloadWeapon;


	// Token: 0x04000192 RID: 402
	private Vector2 userInput;

	// Token: 0x04000193 RID: 403
	private Vector3 rootMotion;

	// Token: 0x04000194 RID: 404
	private Vector3 velocity;

	// Token: 0x04000195 RID: 405
	private bool isJumping;

	// Token: 0x04000196 RID: 406
	private bool isCrouching;

	// Token: 0x04000197 RID: 407
	public bool checkTheChange;

	// Token: 0x04000198 RID: 408
	private int isSprintingParam = Animator.StringToHash("IsSprinting");
    private int isSprintingUnarmParam = Animator.StringToHash("IsSprintingUnarm");

    // Token: 0x04000199 RID: 409
    private int isCrouchingParam = Animator.StringToHash("IsCrouching");

	// Token: 0x0400019A RID: 410
	private int isJumpingParam = Animator.StringToHash("IsJumping");


	// Token: 0x0400019E RID: 414
	public float maxEnergy;

	// Token: 0x0400019F RID: 415
	public float energy;

	// Token: 0x040001A0 RID: 416
	private bool recover;
}
