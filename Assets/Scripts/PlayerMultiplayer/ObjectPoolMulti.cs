using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class ObjectPoolMulti : MonoBehaviourPun
{
    [HideInInspector]
    public List<Bullet> poolObjects;
    public Bullet objectToPool;
    private int amountToPool =50;
    // Start is called before the first frame update
    void Start()
    {
        poolObjects = GetComponentsInChildren<Bullet>().ToList<Bullet>();
        //this.transform.SetParent(null, false);
        //for (int i = 0; i < this.amountToPool; i++)
        //{
        //    Bullet bullet = UnityEngine.Object.Instantiate<Bullet>(this.objectToPool, base.transform, true);
        //    bullet.Deactive();
        //    this.poolObjects.Add(bullet);
        //}
    }
    public Bullet GetPooledObject()
    {
        for (int i = 0; i < this.amountToPool; i++)
        {
            if (!this.poolObjects[i].IsActive)
            {
                return this.poolObjects[i];
            }
        }
        return null;
    }
}
