using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float power;

    private Rigidbody _rb;
    private OVRGrabbable _grabbable;

    void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _grabbable = GetComponentInParent<OVRGrabbable>();
    }

    void Update()
    {
        if (gameObject.transform.parent.parent != null)
        {
            if (_grabbable.isGrabbed)
            {
                gameObject.transform.parent.SetParent(null);
            }
        }
        else
        {
            if (!_grabbable.isGrabbed)
            {
                _rb.isKinematic = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBody"))
        {
            other.transform.parent.GetComponent<EnemyControl>().EnemyAttacked(power);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInParent<EnemyControl2>().EnemyAttacked(power);
            Destroy(transform.root.gameObject, 0.5f);
        }
    }
}