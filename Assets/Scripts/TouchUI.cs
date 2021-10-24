using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUI : MonoBehaviour
{
    [SerializeField]
    private Transform finger;

    [SerializeField]
    private RectTransform ui;

    private Vector3 lastPos;
    void Start()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("raytest"))
        {
            lastPos = finger.position;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("raytest"))
        {
            Vector3 uiPos = ui.position;
            uiPos.y += finger.position.y - lastPos.y;
            ui.position = uiPos;
            lastPos = finger.position;
        }
    }
}
