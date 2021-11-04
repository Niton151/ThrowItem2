using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGrenade : MonoBehaviour
{
    private static int returnBaseCount = 0;

    Rigidbody rb; 

    [SerializeField]
    private GameObject player;

    private bool isRunning = false;

    private bool isBase = true;

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

    [SerializeField]
    private GameObject itemSpawn;

    [SerializeField]
    private GameObject enemySpawn;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip teleportSound;

    [SerializeField]
    private AudioClip holeSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isBase == false)
        {
            timer += Time.deltaTime;
            if(timer >= runTime * 100)
            {
                if (isRunning)
                {
                    TeleportItems();
                }

                if (OVRInput.GetDown(OVRInput.RawButton.B) || Input.GetKeyDown(KeyCode.Space))
                {
                    ReturnBase(true);
                }
            }
        }   

        if(this.gameObject.transform.position.y <= 3)
        {
            rb.isKinematic = true;
            Tutorial.IntoIsGround(true);
            if ((OVRInput.GetDown(OVRInput.RawButton.B) || Input.GetKeyDown(KeyCode.Space)) && isRunning == false)
            {
                TeleportPlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
            itemSpawn.GetComponent<ItemSpawn>().Spawn();
            enemySpawn.GetComponent<ItemSpawn>().Spawn();           
        }  
    }

    private void TeleportPlayer()
    {
        audioSource.PlayOneShot(teleportSound);
        player.transform.position = this.transform.position;
        isRunning = true;
        isBase = false;
        Tutorial.IntoIsRunning(isRunning);
        Tutorial.IntoIsBase(isBase);
        blackHole.SetActive(true);
    }

    public void ReturnBase(bool isClear)
    {
        audioSource.PlayOneShot(teleportSound);
        this.transform.position = basePos.position + new Vector3(0, 1, 0);
        player.transform.position = basePos.position;
        rb.isKinematic = false;
        timer = 0;
        if (isClear)
        {
            returnBaseCount++;
            Tutorial.IntoReturnCount(returnBaseCount);
        }
        core.SetActive(false);
        blackHole.SetActive(false);
        isBase = true;
        Tutorial.IntoIsBase(isBase);
    }

    private void TeleportItems()
    {
        audioSource.PlayOneShot(holeSound);
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

    public void IntoIsBase(bool ib)
    {

    }
}
