using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float maxHp;

    private float hp;

    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private GameObject Rhand;

    [SerializeField]
    private GameObject Lhand;

    [SerializeField]
    private AudioClip reloadOutSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        hp = maxHp;
        hpSlider.value = hp / maxHp;
    }


    void Update()
    {
        OVRGrabbable RhandObj = Rhand.GetComponent<OVRGrabber>().grabbedObject;
        OVRGrabbable LhandObj = Lhand.GetComponent<OVRGrabber>().grabbedObject;
        if (RhandObj != null && RhandObj.gameObject.CompareTag("Gun"))
        {
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                RhandObj.gameObject.GetComponent<LongRangeWeapon>().MagCheck();
                Debug.Log("magcheck!");
            }

            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                RhandObj.gameObject.GetComponentInChildren<Magazine>().RemoveMag();
                audioSource.PlayOneShot(reloadOutSound);
            }
        }

        if (LhandObj != null && LhandObj.gameObject.CompareTag("Gun"))
        {
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
            {
                LhandObj.gameObject.GetComponent<LongRangeWeapon>().MagCheck();
            }

            if (OVRInput.GetDown(OVRInput.RawButton.X))
            {
                LhandObj.gameObject.GetComponentInChildren<Magazine>().RemoveMag();
                audioSource.PlayOneShot(reloadOutSound);
            }
        }
    }

    public float GetPlayerHP()
    {
        return hp;
    }

    public float GetPlayerMaxHP()
    {
        return maxHp;
    }

    public void PlayerAttacked(float damage)
    {
        hp -= damage;
        hpSlider.value = hp / maxHp;
        if (this.hp <= 0)
        {
            gameObject.SetActive(false);
        }

        if (hp >= maxHp)
        {
            hp = maxHp;
        }
    }
}
