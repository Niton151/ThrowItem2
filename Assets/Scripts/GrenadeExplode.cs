using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplode : MonoBehaviour
{
    [SerializeField]
    private float power;

    void Start()
    {
        
    }

    // Update is called once per frame
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
            other.GetComponent<EnemyControl2>().EnemyAttacked(power);
        }
    }
}
