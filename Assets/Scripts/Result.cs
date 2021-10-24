using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    private Text resultPrinter;
    void Start()
    {
        resultPrinter = GameObject.Find("ResultPrinter").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToTheTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
