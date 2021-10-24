using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LongRangeWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private string magTag;

    [SerializeField]
    private GameObject mag;

    private GameObject newMag;

    private int oldAmmo;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip shootSound;

    [SerializeField]
    private AudioClip reloadInSound;

    private ParticleSystem muzzleFrash;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        muzzleFrash = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (newMag != null)
        {
            float distance = Vector3.Distance(newMag.transform.position, this.transform.position);
            if (distance > 0.15f)
            {
                newMag.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void MagCheck()
    {
        var magScript = GetComponentInChildren<Magazine>();
        if (magScript != null && magScript.ReduceAmmo()) CreatBullet();
    }

    public void CreatBullet()
    {
        Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
        muzzleFrash.Play();
        audioSource.PlayOneShot(shootSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(magTag))
        {
            oldAmmo = other.GetComponent<Magazine>().ammo;
            Destroy(other.gameObject);
            newMag = Instantiate(mag, this.transform.position, this.transform.rotation, this.transform);
            newMag.GetComponent<BoxCollider>().enabled = false;
            newMag.GetComponent<Rigidbody>().isKinematic = true;
            newMag.GetComponent<OVRGrabbable>().enabled = false;
            newMag.GetComponentInChildren<MeshCollider>().enabled = false;
            newMag.GetComponent<Magazine>().AmmoStart(oldAmmo);
            audioSource.PlayOneShot(reloadInSound);
        }
    }
}
