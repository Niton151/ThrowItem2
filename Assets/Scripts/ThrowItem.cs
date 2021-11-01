using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    [SerializeField]
    private int quantity;

    private GameObject canvas;

    private OVRGrabbable grabbable;

    void Start()
    {
        canvas = transform.Find("Sphere/Canvas").gameObject;
        grabbable = GetComponent<OVRGrabbable>();
    }

    
    void Update()
    {
        if (grabbable.grabbedBy) canvas.SetActive(true);
        else canvas.SetActive(false);
    }

    public int GetQuantity()
    {
        return quantity;
    }
}
