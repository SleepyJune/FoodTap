using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.CookingScene
{
    public class CookingRecipeManager : MonoBehaviour
    {
        public Image recipeIcon;
        public CookingIngredientItem ingredientPrefab;
        public Transform ingredientParent;

        [NonSerialized]
        public FoodRecipeObject currentRecipe;

        Dictionary<int, CookingIngredientItem> ingredients = new Dictionary<int, CookingIngredientItem>();

        [NonSerialized]
        public int maxIngredientCount = 2;

        void Start()
        {
            Restart();
        }

        public void Restart()
        {
            GenerateNewRecipe();
        }

        void GenerateNewRecipe()
        {
            currentRecipe = GameManager.instance.foodManager.GenerateRandomRecipe();

            recipeIcon.sprite = currentRecipe.dish.icon;

            ingredientParent.DeleteChildren();

            ingredients = new Dictionary<int, CookingIngredientItem>();

            foreach (var ingredient in currentRecipe.ingredients)
            {
                var newItem = Instantiate(ingredientPrefab, ingredientParent);
                var count = UnityEngine.Random.Range(1, maxIngredientCount);

                newItem.SetIngredient(ingredient, count);

                ingredients.Add(ingredient.id, newItem);
            }
        }

        public FoodObject GenerateRandomIngredient()
        {
            if (currentRecipe)
            {
                var index = UnityEngine.Random.Range(0, currentRecipe.ingredients.Length);

                return currentRecipe.ingredients[index];
            }
            else
            {
                return null;
            }
        }

        public bool AddFood(CookingFoodItem food)
        {
            CookingIngredientItem ingredient;
            if (ingredients.TryGetValue(food.food.id, out ingredient))
            {
                ingredient.DecreaseCount();

                if (CheckRecipe())
                {
                    GameManager.instance.Victory();
                }

                return true;
            }

            return false;
        }

        bool CheckRecipe()
        {
            foreach (var item in ingredients.Values)
            {
                if (item.count != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
