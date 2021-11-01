using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField]
    private float maxCount;

    [SerializeField]
    private float addCount;

    [SerializeField]
    private List<GameObject> items;

    [SerializeField]
    private float radius;

    [SerializeField]
    private GameObject enemy;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
        for(int i = 0; i < maxCount; i++)
        {
            var rpos = this.transform.position + RandomPosition.RandomPos(radius);
            Instantiate(RandomPosition.RandomInList(items), new Vector3(rpos.x, 10f, rpos.z), Quaternion.Euler(new Vector3(0, 0, Random.Range(-180f, 180f))));
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void AddEnemy()
    {
        items.Add(enemy);
    }
}
