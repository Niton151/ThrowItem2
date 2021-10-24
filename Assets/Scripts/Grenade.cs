using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private float detonateTime;

    private float timer;

    [SerializeField]
    private GameObject explodeRange;

    [SerializeField]
    private GameObject pin;

    [SerializeField]
    private GameObject body;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip explodeSound;

    [SerializeField]
    private AudioClip pinSound;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, pin.transform.position) >= 0.15f)
        {
            if (pinSound != null)
            {
                audioSource.PlayOneShot(pinSound);
                pinSound = null;
            }
            
            if (this.GetComponent<OVRGrabbable>().isGrabbed == false)
            {
                timer += Time.deltaTime;
                if (timer >= detonateTime)
                {
                    Explode();
                    timer = 0;
                }
            }

        }
    }

    private void Explode()
    {
        audioSource.PlayOneShot(explodeSound);
        explodeRange.SetActive(true);
        Destroy(body.gameObject);
        Destroy(this.gameObject, 2f);
    }
}
