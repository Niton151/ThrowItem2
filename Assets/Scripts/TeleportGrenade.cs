using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGrenade : MonoBehaviour
{
    Rigidbody rb; 

    OVRGrabbable grabbable;

    [SerializeField]
    private GameObject player;

    private bool isRunning;

    private float timer;

    private float runTime;

    [SerializeField]
    private Transform basePos;

    [SerializeField]
    private GameObject blackHole;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabbable = GetComponent<OVRGrabbable>();
    }

    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            if(timer >= runTime && grabbable.grabbedBy)
            {
                ReturnBase();
            }
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Teleport();
        }
    }

    private void Teleport()
    {
        rb.isKinematic = true;
        player.transform.position = this.transform.position;
        isRunning = true;
        blackHole.SetActive(true);
    }

    private void ReturnBase()
    {
        this.transform.position = basePos.position;
        player.transform.position = basePos.position;
        rb.isKinematic = false;
        isRunning = false;
        timer = 0;
        blackHole.SetActive(false);
    }
}
