using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReload : MonoBehaviour
{
    public Animator rigController;
    public WeaponAnimationEvent animationEvents;
    public ActiveWeapon activeWeapon;
    public Transform leftHand;
    public bool isReloading;

    private GameObject magazineHand;
    private float timeDestroyDroppedMagazine;

    void Start()
    {
        animationEvents.WeaponAnimEvent.AddListener(OnAnimationEvent);
        if (DataManager.HasInstance())
        {
            timeDestroyDroppedMagazine = DataManager.Instance.GlobalConfig.timeDestroyDroppedMagazine;
        }
    }

    void Update()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        if (weapon)
        {
            if (Input.GetKeyDown(KeyCode.R) || weapon.CanReload())
            {
                isReloading = true;
                rigController.SetTrigger("reload_weapon");
            }
        }
        
    }

    private void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "detach_magazine":
                DetachMagazine();
                break;
            case "drop_magazine":
                DropMagazine();
                break;
            case "refill_magazine":
                RefillMagazine();
                break;
            case "attach_magazine":
                AttachMagazine();
                break;  
        }
    }

    private void DetachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
    }

    private void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.transform.localScale = Vector3.one;
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        Destroy(droppedMagazine, timeDestroyDroppedMagazine);
        magazineHand.SetActive(false);
    }

    private void RefillMagazine()
    {
        magazineHand.SetActive(true);
    }

    private void AttachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.RefillAmmo();
        rigController.ResetTrigger("reload_weapon");
        if (ListenerManager.HasInstance())
        {
            ListenerManager.Instance.BroadCast(ListenType.UPDATE_AMMO, weapon);
        }
        isReloading = false;
    }
}
