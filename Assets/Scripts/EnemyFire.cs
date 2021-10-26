using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private float time;

    [SerializeField]
    private float interval;

    [SerializeField]
    private float power;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            time += Time.deltaTime;
            if(time >= interval)
            {
                other.GetComponent<PlayerControl>().PlayerAttacked(power);
                time = 0;
            }
        }
    }
}
