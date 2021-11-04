using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(Bullet))]
#endif

public class PlayerBullet : Bullet
{
    [SerializeField]
    private GameObject spark;
    
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
        if (other.gameObject.CompareTag("EnemyBody"))
        {
            other.transform.parent.gameObject.GetComponent<EnemyControl>().EnemyAttacked(bulletPower);
            spark.SetActive(true);
            Destroy(this.gameObject, 0.5f);
        }
    }
}
