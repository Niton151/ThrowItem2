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

    [SerializeField]
    private ParticleSystem[] goalEffect;

    void Start()
    {
        supervisor = GameObject.Find("SupervisorObj").GetComponent<Supervisor>();
        craftSystem = GameObject.Find("CraftSystem").GetComponent<CraftSystem>();
        goalEffect = GetComponentsInChildren<ParticleSystem>();
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
            Destroy(other.transform.root.gameObject);
            audioSource.PlayOneShot(correctSound);
            foreach(var p in goalEffect)
            {
                p.Play();
            }
        }

        else
        {
            audioSource.PlayOneShot(wrongSound);
        }
    }
}
