

using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class WeaponRaycastMulti : MonoBehaviourPun
{
    public ObjectPoolMulti pool;
    public BulletMulti curBullet;
    // Token: 0x06000201 RID: 513 RVA: 0x0000B084 File Offset: 0x00009284
    private void Start()
    {
        if (BaseManager<DataManager>.HasInstance())
        {
            this.gunInfo = BaseManager<DataManager>.Instance.GetInfo(this.weaponName);
        }
        this.ammoCount = this.gunInfo[KeyInfo.maxAmmo];
        this.totalAmmo = this.gunInfo[KeyInfo.maxAmmo] * 4;
        this.fireRate = (float)this.gunInfo[KeyInfo.fireRate];
        this.initFireRate = this.fireRate;
    }

    // Token: 0x06000202 RID: 514 RVA: 0x0000B0F3 File Offset: 0x000092F3
    public void SingleShot(Vector3 target)
    {
        this.FireBullet(target);
    }

    // Token: 0x06000203 RID: 515 RVA: 0x0000B0FC File Offset: 0x000092FC


    public void StartFiring()
    {
        this.fireRate = this.initFireRate;
        this.isFiring = true;
        if (this.accumulatedTime > 0f)
        {
            this.accumulatedTime = 0f;
        }
        WeaponRecoilMulti weaponRecoil = this.weaponRecoil;
        if (weaponRecoil == null)
        {
            return;
        }
        weaponRecoil.Reset();
    }

    // Token: 0x06000204 RID: 516 RVA: 0x0000B139 File Offset: 0x00009339
    public void UpdateWeapon(float deltaTime, Vector3 target)
    {
        if (this.isFiring)
        {
            this.UpdateFiring(deltaTime, target);
        }
        else
        {
            this.accumulatedTime += deltaTime;
        }
        this.UpdateBullets(deltaTime);
    }

    // Token: 0x06000205 RID: 517 RVA: 0x0000B164 File Offset: 0x00009364
    private void UpdateFiring(float deltaTime, Vector3 target)
    {
        this.accumulatedTime += deltaTime;
        float num = 1f / this.fireRate;
        while (this.accumulatedTime >= 0f)
        {
            this.FireBullet(target);
            //photonView.RPC("FireBullet", RpcTarget.All, target);
            this.accumulatedTime -= num;
        }
    }

    // Token: 0x06000206 RID: 518 RVA: 0x0000B1B0 File Offset: 0x000093B0
    public void StopFiring()
    {
        this.isFiring = false;
    }

    // Token: 0x06000207 RID: 519 RVA: 0x0000B1BC File Offset: 0x000093BC
    public void UpdateBullets(float deltaTime)
    {

        if (this.equipBy == EquipBy.Player)
        {
            pool.poolObjects.ForEach(delegate (BulletMulti bullet)
            {
                Vector3 position = this.GetPosition(bullet);
                bullet.time += deltaTime;
                Vector3 position2 = this.GetPosition(bullet);
                //photonView.RPC("RaycastSegment", RpcTarget.All, position, position2, bullet);
                this.RaycastSegment(position, position2, bullet);
            });
        }
        //photonView.RPC("DestroyBullets", RpcTarget.All);
        this.DestroyBullets();
    }

    // Token: 0x06000208 RID: 520 RVA: 0x0000B224 File Offset: 0x00009424

    private void DestroyBullets()
    {
        if (this.equipBy == EquipBy.Player)
        {
            //using (List<BulletMulti>.Enumerator enumerator = pool.poolObjects.GetEnumerator())
            //{
            //	while (enumerator.MoveNext())
            //	{
            //		BulletMulti bullet = enumerator.Current;
            //		if (bullet.time > this.maxLifetime)
            //		{
            //			//bullet.Deactive(raycastOrigin.position);

            //			photonView.RPC("Deactive",RpcTarget.All, pool.poolObjects.FindInstanceID(bullet), raycastOrigin.position);
            //		}
            //	}
            //	return;
            //}
            int ind = pool.GetIndexPooledObjectDeactive();
            if (ind >= 0)
            {
                photonView.RPC("Deactive", RpcTarget.All, ind, raycastOrigin.position);
            }
        }
    }

    // Token: 0x06000209 RID: 521 RVA: 0x0000B2E4 File Offset: 0x000094E4
    //[PunRPC]
    private void FireBullet(Vector3 target)
    {
        if (this.ammoCount <= 0)
        {
            this.isFiring = false;
            return;
        }
        this.ammoCount--;
        if (BaseManager<ListenerManager>.HasInstance())
        {
            BaseManager<ListenerManager>.Instance.BroadCast(ListenType.UPDATE_AMMO, this);
        }
        //foreach (ParticleSystem particleSystem in this.muzzleFlash)
        //{
        //	particleSystem.Emit(particleSystem.maxParticles);
        //}
        photonView.RPC("EmitMuzzle", RpcTarget.All);
        Vector3 velocity = (target - this.raycastOrigin.position).normalized * (float)this.gunInfo[KeyInfo.bulletSpeed];
        {
            if (this.equipBy == EquipBy.Player)
            {
                //pool.GetPooledObject().Active(this.raycastOrigin.position, velocity);

                int ind = pool.GetIndexPooledObject();
                if(ind >= 0)
                {
                    photonView.RPC("Active", RpcTarget.All,ind, this.raycastOrigin.position, velocity);
                }
                
            }
        }

        WeaponRecoilMulti weaponRecoil = this.weaponRecoil;
        if (weaponRecoil != null)
        {
            weaponRecoil.GenerateRecoil(this.weaponName);
        }
    }
    // Token: 0x0600020A RID: 522 RVA: 0x0000B414 File Offset: 0x00009614
    private Vector3 GetPosition(BulletMulti bullet)
    {
        Vector3 a = Vector3.down * this.bulletDrop;
        return bullet.initialPosition + bullet.initialVelocity * bullet.time + 0.5f * a * bullet.time * bullet.time;
    }

    // Token: 0x0600020B RID: 523 RVA: 0x0000B474 File Offset: 0x00009674

    private void RaycastSegment(Vector3 start, Vector3 end, BulletMulti bullet)
    {
        Vector3 direction = end - start;
        float magnitude = direction.magnitude;
        this.ray.origin = start;
        this.ray.direction = direction;
        if (Physics.Raycast(this.ray, out this.hitInfo, magnitude, this.hitLayer))
        {
            foreach (var effect in hitEffect)
            {
                effect.transform.position = this.hitInfo.point;
                effect.transform.forward = this.hitInfo.normal;
                effect.Emit(effect.maxParticles);
            }

            bullet.transform.position = this.hitInfo.point;
            bullet.time = this.maxLifetime-1f;
            end = this.hitInfo.point;
            Rigidbody component = this.hitInfo.collider.GetComponent<Rigidbody>();
            if (component)
            {
                component.AddForceAtPosition(this.ray.direction * 10f, this.hitInfo.point, ForceMode.Impulse);
            }
            HitBoxMulti component2 = this.hitInfo.collider.GetComponent<HitBoxMulti>();
            if (component2)
            {
                component2.OnHit(this, this.ray.direction);
            }
        }
        bullet.transform.position = end;
    }

    // Token: 0x040001F4 RID: 500
    public LayerMask hitLayer;

    // Token: 0x040001F5 RID: 501
    public string weaponName;

    // Token: 0x040001F6 RID: 502
    public ActiveWeaponMultiplayer.WeaponSlot weaponSlot;

    // Token: 0x040001F7 RID: 503
    public bool isFiring;

    // Token: 0x040001F8 RID: 504
    public float bulletDrop;

    // Token: 0x040001F9 RID: 505
    public ParticleSystem[] muzzleFlash;

    // Token: 0x040001FA RID: 506
    public ParticleSystem[] hitEffect;

    // Token: 0x040001FB RID: 507
    public Transform raycastOrigin;

    // Token: 0x040001FC RID: 508
    [HideInInspector]
    public Transform raycastDestination;

    // Token: 0x040001FD RID: 509
    private Ray ray;

    // Token: 0x040001FE RID: 510
    private RaycastHit hitInfo;

    // Token: 0x040001FF RID: 511
    private float accumulatedTime;

    // Token: 0x04000200 RID: 512
    private float maxLifetime = 3f;

    // Token: 0x04000201 RID: 513
    public int ammoCount;

    // Token: 0x04000202 RID: 514
    public int totalAmmo;

    // Token: 0x04000203 RID: 515
    public GameObject magazine;

    // Token: 0x04000204 RID: 516
    public GameObject meshWeapon;

    // Token: 0x04000205 RID: 517
    public WeaponRecoilMulti weaponRecoil;

    // Token: 0x04000206 RID: 518
    public Dictionary<KeyInfo, int> gunInfo;

    // Token: 0x04000207 RID: 519
    public EquipBy equipBy;

    // Token: 0x04000208 RID: 520
    public List<ShootingMode> mode;

    // Token: 0x04000209 RID: 521
    public string shooter;

    // Token: 0x0400020A RID: 522
    public float fireRate;

    // Token: 0x0400020B RID: 523
    private float initFireRate;

    // Token: 0x0400020C RID: 524
    public AudioSource audio;
}
