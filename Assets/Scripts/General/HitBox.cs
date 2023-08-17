using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health health;
    public Rigidbody rb;

    public void OnHit(RaycastWeapon weapon, Vector3 direction)
    {
        ////float dame = weapon.gunInfo[KeyInfo.damage];
        //if (this.tag.Equals("Head"))
        //{
        //    dame *= 2;
        //}
        ////health.shooter = weapon.shooter;

        //health.TakeDamage(dame, direction, rb);
    }
}
