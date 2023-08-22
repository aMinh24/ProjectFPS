
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000034 RID: 52
public class AiAgent : MonoBehaviour
{

	// Token: 0x040000F5 RID: 245
	public AIStateID initState;



    // Token: 0x040000F6 RID: 246
    [HideInInspector]
    public AiStateMachine stateMachine;

    // Token: 0x040000F7 RID: 247
    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    // Token: 0x040000F8 RID: 248
    public Transform spinePlayerTransform;

    // Token: 0x040000F9 RID: 249
    public Transform playerTransform;

    // Token: 0x040000FA RID: 250
    [HideInInspector]
    public AiWeaponIK weaponIK;

    // Token: 0x040000FB RID: 251
    [HideInInspector]
    public Animator animator;

    // Token: 0x040000FC RID: 252
    [HideInInspector]
    public AiWeapon weapon;

    // Token: 0x040000FD RID: 253
    [HideInInspector]
    public Ragdoll ragdoll;

    // Token: 0x040000FE RID: 254
    [HideInInspector]
    public AiHealth health;

    // Token: 0x040000FF RID: 255
    public GameObject markUI;
    // Token: 0x0600009F RID: 159 RVA: 0x00005500 File Offset: 0x00003700
    private void Awake()
	{
		this.weaponIK = base.GetComponent<AiWeaponIK>();
		this.animator = base.GetComponent<Animator>();
		this.weapon = base.GetComponent<AiWeapon>();
		this.ragdoll = base.GetComponent<Ragdoll>();
		this.health = base.GetComponent<AiHealth>();
		this.navMeshAgent = base.GetComponent<NavMeshAgent>();
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00005558 File Offset: 0x00003758
	private void Start()
	{
		if (this.spinePlayerTransform == null)
		{
			this.spinePlayerTransform = UnityEngine.Object.FindFirstObjectByType<PlayerHealth>().spine;
			this.playerTransform = UnityEngine.Object.FindFirstObjectByType<PlayerHealth>().transform;
		}
		this.stateMachine = new AiStateMachine(this);
		this.stateMachine.RegisterState(new AIStateIdle());
		this.stateMachine.RegisterState(new AIStateChasePlayer());
		this.stateMachine.RegisterState(new AIStateAttackPlayer());
		this.stateMachine.RegisterState(new AIStateDeath());
		this.stateMachine.ChangeState(this.initState);
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x000055F0 File Offset: 0x000037F0
	private void Update()
	{
		Debug.Log(stateMachine.currentState);
		this.stateMachine.Update();
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00005600 File Offset: 0x00003800
	public void DisableAll()
	{
		this.animator.enabled = false;
		this.weapon.enabled = false;
		this.weaponIK.enabled = false;
		this.ragdoll.enabled = false;
		this.navMeshAgent.enabled = false;
		this.markUI.gameObject.SetActive(false);
		base.StartCoroutine(this.health.OnDestroyWhenDeath());
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000566C File Offset: 0x0000386C
	public IEnumerator EnableAll()
	{
		yield return new WaitForSeconds(60f);
		this.animator.enabled = true;
		this.markUI.gameObject.SetActive(true);
		this.weapon.enabled = true;
		WeaponRaycast weaponRaycast = this.weapon.weapon;
		weaponRaycast.gameObject.transform.SetParent(base.transform, false);
		weaponRaycast.GetComponent<BoxCollider>().isTrigger = true;
		weaponRaycast.GetComponent<BoxCollider>().enabled = false;
		weaponRaycast.GetComponent<Rigidbody>().useGravity = false;
		this.weaponIK.enabled = true;
		this.ragdoll.enabled = true;
		this.ragdoll.DeactiveRagdoll();
		this.navMeshAgent.enabled = true;
		if (BaseManager<DataManager>.HasInstance())
		{
			this.health.currentHealth = BaseManager<DataManager>.Instance.GlobalConfig.maxHealth;
			this.health.isHitting = false;
			this.health.isDead = false;
		}
		this.stateMachine.ChangeState(this.initState);
		base.gameObject.SetActive(true);
		yield break;
	}

	
}
