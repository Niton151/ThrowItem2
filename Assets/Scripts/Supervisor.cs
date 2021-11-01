using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class Supervisor : MonoBehaviour
{
    private int[] itemCount;
    [SerializeField]
    private List<Text> countText;

    public enum Item
    {
        wood,
        metal,
        plastic,
        glass,
        food,//H‚×Žc‚µ
        cloth
    };

    void Start()
    {
        itemCount = new int[Enum.GetNames(typeof(Item)).Length];
    }

    void Update()
    {

    }

    public void ItemCountPrint()
    {
        foreach (var txt in countText.Select((value, index) => new { value, index }))
        {
            txt.value.text = itemCount[txt.index].ToString();
        }
    }

    public int[] GetItemCount()
    {
        return itemCount;
    }
}
