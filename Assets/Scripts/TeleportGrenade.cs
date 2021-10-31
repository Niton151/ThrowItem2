using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGrenade : MonoBehaviour
{
    private static int returnBaseCount = 0;

    Rigidbody rb; 

    OVRGrabbable grabbable;

    [SerializeField]
    private GameObject player;

    private bool isRunning;

    private bool isBase;

    private float timer = 0;

    private float runTime = 0.15f;

    [SerializeField]
    private Transform basePos;

    [SerializeField]
    private Transform craftPos;

    [SerializeField]
    private GameObject blackHole;

    [SerializeField]
    private GameObject core;

    private bool isGround = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabbable = GetComponent<OVRGrabbable>();
    }

    void Update()
    {
        if (isBase == false)
        {
            timer += Time.deltaTime;
            if(timer >= runTime * 100)
            {
                TeleportItems();
                if (grabbable.grabbedBy || Input.GetKeyDown(KeyCode.Space))
                {
                    ReturnBase();
                }
            }
        }   
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Tutorial.IntoIsGround(true);
            if (OVRInput.GetDown(OVRInput.RawButton.B) || Input.GetKeyDown(KeyCode.Space))
            {
                TeleportPlayer();
            }
        }
    }

    private void TeleportPlayer()
    {
        player.transform.position = this.transform.position;
        rb.isKinematic = true;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
        isRunning = true;
        isBase = false;
        Tutorial.IntoIsRunning(isRunning);
        Tutorial.IntoIsBase(isBase);
        blackHole.SetActive(true);
    }

    private void ReturnBase()
    {
        this.transform.position = basePos.position;
        player.transform.position = basePos.position;
        rb.isKinematic = false;
        timer = 0;
        returnBaseCount++;
        Tutorial.IntoReturnCount(returnBaseCount);
        core.SetActive(false);
        blackHole.SetActive(false);
        isBase = true;
        Tutorial.IntoIsBase(isBase);
    }

    private void TeleportItems()
    {
        isRunning = false;
        Tutorial.IntoIsRunning(isRunning);
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
