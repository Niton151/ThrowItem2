using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(Bullet))]
#endif

public class PlayerBullet : Bullet
{
    
    public override void Start()
    {
        base.Start();
    }

    
    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyControl>().EnemyAttacked(bulletPower);
            Destroy(this.gameObject);
        }
    }
}
