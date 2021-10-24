using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    private float timer = 0;

    [SerializeField]
    private float interval;

    [SerializeField]
    private List<GameObject> items;

    [SerializeField]
    private Transform rangeA;

    [SerializeField]
    private Transform rangeB;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            Spawn();
            timer = 0;
        }
    }

    private void Spawn()
    {
        Instantiate(RandomPosition.RandomInList(items), RandomPosition.RandomPos(rangeA, rangeB), Quaternion.Euler(new Vector3(0, 0, Random.Range(-180f, 180f))));
    }
}
