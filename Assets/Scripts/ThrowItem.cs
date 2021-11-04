using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    [SerializeField]
    private int quantity;

    private GameObject canvas;

    private OVRGrabbable grabbable;
   
    private GameObject teleGre;

    void Start()
    {
        canvas = transform.Find("Sphere/Canvas").gameObject;
        grabbable = transform.root.GetComponent<OVRGrabbable>();
        teleGre = GameObject.Find("TeleportGrenade");
    }

    
    void Update()
    {
        if (grabbable.grabbedBy) canvas.SetActive(true);
        else canvas.SetActive(false);

        if(4200 < this.transform.position.y && this.transform.position.y < 4250)
        {
            this.transform.root.transform.position = teleGre.GetComponent<TeleportGrenade>().GetCraftPos().position;
        }

        if(this.transform.position.y < -5)
        {
            this.transform.root.transform.position = new Vector3(this.transform.position.x, 10f, this.transform.position.z);
        }
    }

    public int GetQuantity()
    {
        return quantity;
    }
}
