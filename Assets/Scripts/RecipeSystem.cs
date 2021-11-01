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
    
    void Start()
    {
        supervisor = GameObject.Find("SupervisorObj").GetComponent<Supervisor>();
        craftSystem = GameObject.Find("CraftSystem").GetComponent<CraftSystem>();
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
            Instantiate(craftItem, new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
            if(craftItemName == "íçéÀäÌ")
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
        //îzóÒÇãKíËÇÃëÂÇ´Ç≥Ç…ïœçX
        if (requiredQuantity.Length < haveQuantity.Length)
        {
            Array.Resize(ref requiredQuantity, haveQuantity.Length);
        }

        //UIÇê∂ê¨
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
                
                texts[reserveIndex.IndexOf(i)].GetComponentInChildren<Text>().text = (Supervisor.Item)i + " " + haveQuantity[i] + "/" + requiredQuantity[i];
            }
        }
    }
}
