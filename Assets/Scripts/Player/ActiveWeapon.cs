using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ActiveWeapon : MonoBehaviour
{
    public Animator rigController;
    public Transform crosshairTarget;
    public Transform[] weaponSlots;
    public CharacterAiming characterAiming;
    public WeaponReload reload;
    public bool isChangingWeapon;
    public bool canFire;

    private RaycastWeapon[] equippedWeapons = new RaycastWeapon[2];
    private int activeWeaponIdx;
    private bool isHolstered = false;

    void Start()
    {
        reload = GetComponent<WeaponReload>();
        RaycastWeapon existWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existWeapon)
        {
            Equip(existWeapon);
        }
    }

    void Update()
    {
        var raycastWeapon = GetWeapon(activeWeaponIdx);
        bool isNotSprinting = rigController.GetCurrentAnimatorStateInfo(2).shortNameHash == Animator.StringToHash("notSprinting");
        canFire = !isHolstered && isNotSprinting && !reload.isReloading;
        if (raycastWeapon)
        {
            if (Input.GetButtonDown("Fire1") && canFire && !raycastWeapon.isFiring)
            {
                raycastWeapon.StartFiring();
            }

            raycastWeapon.UpdateWeapon(Time.deltaTime, crosshairTarget.position);

            if (Input.GetButtonUp("Fire1") || !canFire)
            {
                raycastWeapon.StopFiring();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetActiveWeapon(WeaponSlot.Primary);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetActiveWeapon(WeaponSlot.Secondary);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                ToggleActiveWeapon();
            }
        }
    }

    public bool IsFiring()
    {
        RaycastWeapon currentWeapon = GetActiveWeapon();
        if (!currentWeapon)
        {
            return false;
        }

        return currentWeapon.isFiring;
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var raycastWeapon = GetWeapon(weaponSlotIndex);
        if (raycastWeapon)
        {
            Destroy(raycastWeapon.gameObject);
        }
        raycastWeapon = newWeapon;
        raycastWeapon.raycastDestination = crosshairTarget;
        //raycastWeapon.weaponRecoil.characterAiming = characterAiming;
        raycastWeapon.weaponRecoil.rigController = rigController;
        raycastWeapon.transform.SetParent(weaponSlots[weaponSlotIndex],false);
        equippedWeapons[weaponSlotIndex] = raycastWeapon;
        SetActiveWeapon(newWeapon.weaponSlot);
        if (ListenerManager.HasInstance())
        {
            ListenerManager.Instance.BroadCast(ListenType.UPDATE_AMMO, raycastWeapon);
        }
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(activeWeaponIdx);
    }

    public void DropWeapon()
    {
        var currentWeapon = GetActiveWeapon();
        if (currentWeapon)
        {
            currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            equippedWeapons[activeWeaponIdx] = null;
        }
    }

    private void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIdx;
        int activateIndex = (int)weaponSlot;

        if(holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    private void ToggleActiveWeapon()
    {
        bool isHolstered = rigController.GetBool("holster_weapon");
        if (isHolstered)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIdx));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeWeaponIdx));
        }
    }

    private RaycastWeapon GetWeapon(int index)
    {
        if(index < 0 || index >= equippedWeapons.Length)
        {
            return null;
        }
        return equippedWeapons[index];
    }

    private IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        rigController.SetInteger("weapon_index", activateIndex);
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIdx = activateIndex;
    }


    private IEnumerator HolsterWeapon(int index)
    {
        isChangingWeapon = true;
        isHolstered = true;
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", true);
            yield return new WaitForSeconds(0.1f);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
        isChangingWeapon = false;
    }

    private IEnumerator ActivateWeapon(int index)
    {
        isChangingWeapon = true;
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            yield return new WaitForSeconds(0.1f);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            isHolstered = false;
            if (ListenerManager.HasInstance())
            {
                ListenerManager.Instance.BroadCast(ListenType.UPDATE_AMMO, weapon);
            }
        }
        isChangingWeapon = false;
    }

    public void RefillMagazine(int size)
    {
        var weapon = GetActiveWeapon();
        if (weapon)
        {
            weapon.magazineSize += size;
            if (ListenerManager.HasInstance())
            {
                ListenerManager.Instance.BroadCast(ListenType.UPDATE_AMMO, weapon);
            }
        }
    }

}
