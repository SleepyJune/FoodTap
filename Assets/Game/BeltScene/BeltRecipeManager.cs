using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.BeltScene
{
    public class BeltRecipeManager : MonoBehaviour
    {
        public Image recipeIcon;
        public BeltIngredientItem ingredientPrefab;
        public Transform ingredientParent;

        [NonSerialized]
        public FoodRecipeObject currentRecipe;

        Dictionary<int, BeltIngredientItem> ingredients = new Dictionary<int, BeltIngredientItem>();

        void Start()
        {
            GenerateNewRecipe();
        }

        void GenerateNewRecipe()
        {
            currentRecipe = GameManager.instance.foodManager.GenerateRandomRecipe();

            recipeIcon.sprite = currentRecipe.dish.icon;

            ingredientParent.DeleteChildren();

            foreach(var ingredient in currentRecipe.ingredients)
            {
                var newItem = Instantiate(ingredientPrefab, ingredientParent);
                newItem.SetIngredient(ingredient);

                ingredients.Add(ingredient.id, newItem);
            }
        }

        public bool AddFoodFromBelt(FoodBeltItem food)
        {            
            BeltIngredientItem ingredient;
            if(ingredients.TryGetValue(food.food.id, out ingredient))
            {
                ingredient.DecreaseCount();

                CheckRecipe();

                return true;
            }

            return false;
        }

        bool CheckRecipe()
        {
            foreach(var item in ingredients.Values)
            {
                if(item.count != 0)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
