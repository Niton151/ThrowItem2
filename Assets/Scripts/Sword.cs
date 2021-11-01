using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour
{

    //private BoxCollider col;

    //Vector3 lastpos;

    [SerializeField]
    private float atackableSpeed;

    [SerializeField]
    private float power;

    OVRInput.Controller Lcontroller = OVRInput.Controller.LTouch;
    OVRInput.Controller Rcontroller = OVRInput.Controller.RTouch;

    Vector3 L_acc;
    Vector3 R_acc;

    [SerializeField]
    private GameObject Rhand;

    [SerializeField]
    private GameObject Lhand;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip damageSound;

    [SerializeField]
    private AudioClip swingSound;

    void Start()
    {
        //col = this.GetComponent<BoxCollider>();
    }


    void Update()
    {
        //コメントアウトしてる部分は切ったときのコライダー管理
        /*
        if (Lhand.GetComponent<OVRGrabber>().grabbedObject != null && Lhand.GetComponent<OVRGrabber>().grabbedObject.gameObject.CompareTag("Sword") && -L_acc.y > atackableSpeed)
        {
            col.isTrigger = true;
        }
        if (Rhand.GetComponent<OVRGrabber>().grabbedObject != null && Rhand.GetComponent<OVRGrabber>().grabbedObject.gameObject.CompareTag("Sword") && -R_acc.y > atackableSpeed)
        {
            col.isTrigger = true;
        }
        */
    }

    /* private void OnCollisionStay(Collision collision)
     {

         if (Lhand.GetComponent<OVRGrabber>().grabbedObject != null && Lhand.GetComponent<OVRGrabber>().grabbedObject.gameObject.CompareTag("Sword") &&collision.gameObject.CompareTag("Enemy") && -L_acc.y > atackableSpeed)
         {
             col.isTrigger = true;
         }

         if (Rhand.GetComponent<OVRGrabber>().grabbedObject != null && Rhand.GetComponent<OVRGrabber>().grabbedObject.gameObject.CompareTag("Sword") && collision.gameObject.CompareTag("Enemy") && -R_acc.y > atackableSpeed)
         {
             col.isTrigger = true;
         }
     }
    */

    private void OnTriggerEnter(Collider other)
    {
        L_acc = OVRInput.GetLocalControllerVelocity(Lcontroller);
        R_acc = OVRInput.GetLocalControllerVelocity(Rcontroller);
        if (L_acc.magnitude > atackableSpeed && Lhand.GetComponent<OVRGrabber>().grabbedObject != null && Lhand.GetComponent<OVRGrabber>().grabbedObject.gameObject == this.transform.parent.gameObject)
        {
            audioSource.PlayOneShot(swingSound);
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyControl>().EnemyAttacked(power);
                other.GetComponent<EnemyControl2>().EnemyAttacked(power);
                audioSource.PlayOneShot(damageSound);
            }
        }


        if (R_acc.magnitude > atackableSpeed && Rhand.GetComponent<OVRGrabber>().grabbedObject != null && Rhand.GetComponent<OVRGrabber>().grabbedObject.gameObject == this.transform.parent.gameObject)
        {
            audioSource.PlayOneShot(swingSound);
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyControl>().EnemyAttacked(power);
                other.GetComponent<EnemyControl2>().EnemyAttacked(power);
                audioSource.PlayOneShot(damageSound);
            }
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            col.isTrigger = false;
        }
    }
    */
}
