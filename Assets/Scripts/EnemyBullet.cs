using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(Bullet))]
#endif

public class EnemyBullet : Bullet
{
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerControl>().PlayerAttacked(bulletPower);
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            Destroy(this.gameObject);
        }
    }
}
