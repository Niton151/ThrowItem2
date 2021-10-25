using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemyLRW : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip shootSound;

    private ParticleSystem muzzleFrash;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void CreatBullet()
    {
        Instantiate(bullet, this.transform.position, this.transform.rotation);
        audioSource.PlayOneShot(shootSound);
    }
}

