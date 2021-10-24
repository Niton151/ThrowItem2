using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurableObject : MonoBehaviour
{
    [SerializeField]
    private string attackableTagName;

    [SerializeField]
    private float maxDurability;

    private float durability;
    // Start is called before the first frame update
    void Start()
    {
        durability = maxDurability;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(attackableTagName))
        {
            durability--;
            if (durability <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
