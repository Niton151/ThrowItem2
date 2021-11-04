using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    [SerializeField]
    private int quantity;

    private GameObject canvas;

    private OVRGrabbable grabbable;
    
    [SerializeField]
    private GameObject teleGre;

    void Start()
    {
        canvas = transform.Find("Sphere/Canvas").gameObject;
        grabbable = transform.root.GetComponent<OVRGrabbable>();
    }

    
    void Update()
    {
        if (grabbable.grabbedBy) canvas.SetActive(true);
        else canvas.SetActive(false);

        if(4200 < this.transform.position.y && this.transform.position.y < 4250)
        {
            this.transform.position = teleGre.GetComponent<TeleportGrenade>().GetCraftPos().position;
        }
    }

    public int GetQuantity()
    {
        return quantity;
    }
}
