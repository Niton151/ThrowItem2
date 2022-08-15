using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushUI : MonoBehaviour
{
    private Vector3 lastFingerPos = Vector3.zero;

    private Vector3 fingerPos;

    private RectTransform back;

    private bool isStop;

    private float moveJudge = 25f;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        back = GameObject.Find("back").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finger"))
        {
            fingerPos = back.localPosition;
            isStop = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Finger"))
        {
            fingerPos = back.localPosition;
            if (Mathf.Abs(fingerPos.y - lastFingerPos.y) > moveJudge)
            {
                isStop = false;
            }
            lastFingerPos = fingerPos;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Finger") && isStop && timer >= 0.5f)
        {
            this.GetComponentInChildren<RecipeSystem>().CraftItem();
            timer = 0;
        }
    }
}
