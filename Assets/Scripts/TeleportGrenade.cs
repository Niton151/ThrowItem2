using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGrenade : MonoBehaviour
{
    public static int returnCount = 0;

    Rigidbody rb; 

    OVRGrabbable grabbable;

    [SerializeField]
    private GameObject player;

    private bool isRunning;

    private float timer = 0;

    private float runTime = 0.9f;

    [SerializeField]
    private Transform basePos;

    [SerializeField]
    private Transform craftPos;

    [SerializeField]
    private GameObject blackHole;

    [SerializeField]
    private GameObject core;

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
            if(timer >= runTime * 100)
            {
                TeleportItems();
                if (grabbable.grabbedBy)
                {
                    ReturnBase();
                }
            }
        }   
    }

    private void OnCollisionStay(Collision collision)
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A) && collision.gameObject.CompareTag("Ground"))
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
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
        timer = 0;
        core.SetActive(false);
        blackHole.SetActive(false);
    }

    private void TeleportItems()
    {
        isRunning = false;  
    }

    public float GetRunTime()
    {
        return runTime;
    }

    public Transform GetCraftPos()
    {
        return craftPos;
    }

    public GameObject GetCoreObj()
    {
        return core;
    }
}
