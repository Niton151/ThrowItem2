using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float power;

    void Start()
    {
        
    }

    void Update()
    {
        
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
            Destroy(this.gameObject, 0.5f);
        }
    }
}
