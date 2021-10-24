using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{
    private float healAmount;

    [SerializeField]
    private float maxHealAmount;

    [SerializeField]
    private float healInterval;

    private float timer = 0;

    [SerializeField]
    private GameObject playerControl;

    private BoxCollider col;

    [SerializeField]
    private GameObject liquid;

    private GameObject grabObj;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip stingSound;

    [SerializeField]
    private AudioClip recoverSound;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        audioSource = this.GetComponent<AudioSource>();
        healAmount = maxHealAmount;
        liquid.transform.localScale = new Vector3(1f, 1f, healAmount / maxHealAmount);
    }

    void Update()
    {
        grabObj = this.GetComponent<OVRGrabbable>().grabbedBy?.gameObject;

        if (playerControl.GetComponent<PlayerControl>().GetPlayerHP() >= playerControl.GetComponent<PlayerControl>().GetPlayerMaxHP() || healAmount <= 0)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }

        if ((grabObj?.name == "CustomHandRight" && OVRInput.GetUp(OVRInput.RawButton.A)) || (grabObj?.name == "CustomHandLeft" && OVRInput.GetUp(OVRInput.RawButton.X)))
        {
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            audioSource.PlayOneShot(stingSound);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            if((grabObj?.name == "CustomHandRight" && OVRInput.GetDown(OVRInput.RawButton.A)) || (grabObj?.name == "CustomHandLeft" && OVRInput.GetDown(OVRInput.RawButton.X)))
            {
                audioSource.PlayOneShot(recoverSound);
            }

            timer += Time.deltaTime;
            if (timer >= healInterval)
            {
                if((grabObj?.name == "CustomHandRight" && OVRInput.Get(OVRInput.RawButton.A)) || (grabObj?.name == "CustomHandLeft" && OVRInput.Get(OVRInput.RawButton.X)))
                {
                    timer = 0;
                    if (audioSource.isPlaying == false) audioSource.PlayOneShot(recoverSound);
                    Recover();
                }         
            }
        }       
    }

    private void Recover()
    {
        playerControl.GetComponent<PlayerControl>().PlayerAttacked(-1);
        healAmount--;
        liquid.transform.localScale = new Vector3(1f, 1f, healAmount / maxHealAmount);
    }
}
