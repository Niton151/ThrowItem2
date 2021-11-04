using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magazine : MonoBehaviour
{
    [SerializeField]
    private int maxAmmo;

    public int ammo = 100;

    [SerializeField]
    private Text ammoUI;

    

    void Start()
    {
        ammoUI.text = $"{ammo} / {maxAmmo}";
    }

    void Update()
    {
        
    }

    public bool ReduceAmmo()
    {
        if (ammo > 0)
        {
            ammo--;
            ammoUI.text = $"{ammo} / {maxAmmo}";
            return true;
        }
        else return false;
    }

    public void RemoveMag()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.transform.parent = null;
        this.GetComponentInChildren<MeshCollider>().enabled = true;
        this.GetComponent<OVRGrabbable>().enabled = true;
    }

    public void AmmoStart(int oldammo)
    {
        ammo = oldammo;
    }
}
