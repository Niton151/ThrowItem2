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

    [SerializeField] private float timeLimit = 60f;
    private Text timePrinter;

    public enum Item
    {
        wood,
        metal,
        plastic,
        glass,
        leftover,//食べ残し
        cloth
    };

    void Start()
    {
        itemCount = new int[Enum.GetNames(typeof(Item)).Length];

        timePrinter = GameObject.Find("TimePrinter").GetComponent<Text>();
    }

    void Update()
    {
        //制限時間機能
        timeLimit -= Time.deltaTime;
        timePrinter.text = timeLimit.ToString("f1");
        if (timeLimit <= 0)
        {
            timePrinter.text = "時間切れ\nspaceでリザルト";
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene("ResultScene");
        }
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
