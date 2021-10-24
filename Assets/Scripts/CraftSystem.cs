using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSystem : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> recipes;

    [SerializeField]
    private GameObject recipeUI;

    [SerializeField]
    private Transform recipeParent;

    private List<GameObject> reserveRecipes = new List<GameObject>();
    void Start()
    {
        for(int i = 0; i < recipes.Count; i++)
        {
            GameObject obj = Instantiate(recipeUI);
            obj.transform.SetParent(recipeParent, false);
            GameObject obj2 = Instantiate(recipes[i]);
            reserveRecipes.Add(obj2);
            obj2.transform.SetParent(obj.transform, false);
            obj.transform.Find("item_name").GetComponent<Text>().text = recipes[i].GetComponent<RecipeSystem>().GetCraftItemName();
            obj.transform.Find("item_image").GetComponent<Image>().sprite = recipes[i].GetComponent<RecipeSystem>().GetCraftItemSprite();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AccessAllRecipes()
    {
        foreach(var recipe in reserveRecipes)
        {
            recipe.GetComponent<RecipeSystem>().PrintQuantity();
        }
    }
}
