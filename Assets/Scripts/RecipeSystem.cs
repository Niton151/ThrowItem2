using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class RecipeSystem : MonoBehaviour
{
    [SerializeField, EnumIndex(typeof(Supervisor.Item))]
    private int[] requiredQuantity;

    [SerializeField]
    private string craftItemName;

    [SerializeField]
    private Sprite craftItemImage;

    [SerializeField]
    private GameObject craftItem;

    [SerializeField]
    private GameObject craftUI;

    private List<int> reserveIndex = new List<int>();

    private List<GameObject> texts = new List<GameObject>();

    private int[] haveQuantity;

    private Supervisor supervisor;

    private CraftSystem craftSystem;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip craftSound;

    [SerializeField]
    private Transform craftPos;

    private ParticleSystem[] craftEffect;
    
    void Start()
    {
        supervisor = GameObject.Find("SupervisorObj").GetComponent<Supervisor>();
        craftSystem = GameObject.Find("CraftSystem").GetComponent<CraftSystem>();
        craftEffect = craftPos.gameObject.GetComponentsInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        haveQuantity = supervisor.GetItemCount();
        CreatRecipe();
    }

    
    void Update()
    {
        
    }

    public void CraftItem()
    {      
        if (JudgeCraftable() == true) { 
            Instantiate(craftItem, craftPos.position, Quaternion.identity);
            foreach (var p in craftEffect)
            {
                p.Play();
            }

            if (craftItemName == "���ˊ�")
            {
                Tutorial.IntoIsCraftSyringe(true);
            }
            audioSource.PlayOneShot(craftSound);
            for(int i = 0; i < haveQuantity.Length; i++)
            {
                haveQuantity[i] -= requiredQuantity[i];
            }
            craftSystem.AccessAllRecipes();
        }
        
    }

    public void CreatRecipe()
    {
        //�z����K��̑傫���ɕύX
        if (requiredQuantity.Length < haveQuantity.Length)
        {
            Array.Resize(ref requiredQuantity, haveQuantity.Length);
        }

        //UI�𐶐�
        for (int i = 0; i < haveQuantity.Length; i++)
        {
            if (requiredQuantity[i] > 0)
            {
                texts.Add(Instantiate(craftUI));
                texts[texts.Count-1].transform.SetParent(this.transform.parent.Find("materialPanel"), false);
                reserveIndex.Add(i);
            }
        }

        PrintQuantity();
    }

    public string GetCraftItemName()
    {
        return craftItemName;
    }

    public Sprite GetCraftItemSprite()
    {
        return craftItemImage;
    }

    public bool JudgeCraftable()
    {
        for (int i = 0; i < haveQuantity.Length; i++)
        {
            if (reserveIndex.Contains(i))
            {
                if (haveQuantity[i] < requiredQuantity[i])
                {
                    return false;
                }
            }            
        }
        return true;
    }

    public void PrintQuantity()
    {
        for (int i = 0; i < haveQuantity.Length; i++)
        {
            if (reserveIndex.Contains(i))
            {
                
                texts[reserveIndex.IndexOf(i)].GetComponentInChildren<Text>().text = ToString((Supervisor.Item)i) + " " + haveQuantity[i] + "/" + requiredQuantity[i];
            }
        }
    }

    public string ToString(Supervisor.Item item)
    {
        if (item == Supervisor.Item.wood) return "��";
        else if (item == Supervisor.Item.metal) return "����";
        else if (item == Supervisor.Item.plastic) return "�v���X�`�b�N";
        else if (item == Supervisor.Item.glass) return "�K���X";
        else if (item == Supervisor.Item.food) return "�H��";
        else if (item == Supervisor.Item.cloth) return "�z";
        else return base.ToString();
    }
}
