using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector3 RandomPos(Transform rangeA, Transform rangeB)
    {
        float randomX = Random.Range(rangeA.position.x, rangeB.position.x);
        float randomY = Random.Range(rangeA.position.y, rangeB.position.y);
        float randomZ = Random.Range(rangeA.position.z, rangeB.position.z);
        return new Vector3(randomX, randomY, randomZ);
    }

    public static Object RandomInList(List<GameObject> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static Vector3 RandomPos(float radius)
    {
        float randomX = Random.Range(-radius, radius);
        float randomY = Random.Range(-radius, radius);
        float randomZ = Random.Range(-radius, radius);
        return new Vector3(randomX, randomY, randomZ);
    }
}
