using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRange : MonoBehaviour
{
    [SerializeField]
    private Supervisor.Item itemTag;
    private Supervisor supervisor;

    private CraftSystem craftSystem;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip correctSound;

    [SerializeField]
    private AudioClip wrongSound;

    void Start()
    {
        supervisor = GameObject.Find("SupervisorObj").GetComponent<Supervisor>();
        craftSystem = GameObject.Find("CraftSystem").GetComponent<CraftSystem>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(itemTag.ToString()))
        {
            supervisor.GetItemCount()[(int)itemTag] += other.GetComponent<ThrowItem>().GetQuantity();
            craftSystem.AccessAllRecipes();
            Destroy(other.gameObject);
            audioSource.PlayOneShot(correctSound);
        }

        else
        {
            audioSource.PlayOneShot(wrongSound);
        }
    }
}
