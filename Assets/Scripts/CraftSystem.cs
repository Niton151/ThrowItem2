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
    
    [SerializeField] private GameObject handgunRecipe;
    [SerializeField] private GameObject handgunMagRecipe;
    void Start()
    {
        for(int i = 0; i < recipes.Count; i++)
        {
            AddRecipe(recipes[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Tutorial.returnBaseCount == 2)
        {
            AddRecipe(handgunRecipe);
            AddRecipe(handgunMagRecipe);
        }
    }

    public void AccessAllRecipes()
    {
        foreach(var recipe in reserveRecipes)
        {
            recipe.GetComponent<RecipeSystem>().PrintQuantity();
        }
    }

    private void AddRecipe(GameObject recipe)
    {
        GameObject obj = Instantiate(recipeUI);
        obj.transform.SetParent(recipeParent, false);
        GameObject obj2 = Instantiate(recipe);
        reserveRecipes.Add(obj2);
        obj2.transform.SetParent(obj.transform, false);
        obj.transform.Find("item_name").GetComponent<Text>().text = recipe.GetComponent<RecipeSystem>().GetCraftItemName();
        obj.transform.Find("item_image").GetComponent<Image>().sprite = recipe.GetComponent<RecipeSystem>().GetCraftItemSprite();
    }
}
