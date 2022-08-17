using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowString : MonoBehaviour
{
    private bool isPullString = false;

    private bool isMove = false;

    private GameObject arrow;

    private float initialSpeed;

    private OVRGrabbable grabbable;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip shootSound;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isMove)
        {
            this.transform.position = arrow.transform.position;
            //audioSource.volume = this.transform.localPosition.y * -10f;
            if (grabbable.grabbedBy == false && isPullString)
            {
                audioSource.Stop();
                BowShoot();           
            }
        }

        if (this.transform.localPosition.y > -0.01f && isMove)
        {
            isMove = false;
            this.transform.localPosition = new Vector3(0f, -0.01f, 0f);
            audioSource.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow") && other.transform.parent == null && !isMove)
        {
            arrow = other.gameObject;
            grabbable = arrow.GetComponent<OVRGrabbable>();
            isPullString = true;
            isMove = true;
            audioSource.Play();
        }
    }

    private void BowShoot()
    {
        var rb = arrow.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        initialSpeed = -(-0.01f + this.transform.localPosition.y);
        rb.AddRelativeForce(0f, 0f, initialSpeed * 500, ForceMode.Impulse);
        isPullString = false;

        arrow.GetComponent<AudioSource>().PlayOneShot(shootSound);
    }
}
