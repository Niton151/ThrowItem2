using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletType bulletType;

    public float bulletPower;

    public float speed;

    [HideInInspector]
    public Rigidbody rb;

    public enum BulletType
    {
        nomal
    };
    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        //çUåÇéÌóﬁîªíË
        switch (bulletType)
        {
            case BulletType.nomal:
                NormalShoot();
                break;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public void NormalShoot()
    {
        rb.AddForce(transform.forward * speed);
        Destroy(gameObject, 3f);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        
    }
}
