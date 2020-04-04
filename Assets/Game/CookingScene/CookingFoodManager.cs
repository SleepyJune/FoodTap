using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.Game.CookingScene
{
    public class CookingFoodManager : MonoBehaviour
    {
        public CookingFoodItem foodPrefab;
        public CookingFoodItem recipePrefab;

        public Transform foodParent;

        [NonSerialized]
        public List<CookingFoodItem> foodItems = new List<CookingFoodItem>();

        [NonSerialized]
        public float spawnSpeed = 2000;
        [NonSerialized]
        public float cookingSpeedMultiplier = 1.0f;

        [NonSerialized]
        public float lastSpawnTimer = 0;

        int maxFoodCount = 50;

        float maxRadius = 200;

        void Start()
        {
            maxRadius = (foodParent.parent.GetComponent<RectTransform>().rect.width-200) / 2f;
        }

        void Update()
        {
            SpawnFoods();
        }

        public void Restart()
        {
            for(int i = foodItems.Count - 1; i >= 0; i--)
            {
                RemoveFood(foodItems[i]);
            }

            lastSpawnTimer = Time.time;
        }

        void SpawnFoods()
        {
            if (Time.time - lastSpawnTimer > spawnSpeed / 1000.0f)
            {
                CreateNewFood();

                lastSpawnTimer = Time.time;
            }
        }

        void CreateNewFood()
        {
            if(foodItems.Count >= maxFoodCount || GameManager.instance.cookingRecipeManager.currentRecipe == null)
            {
                return;
            }

            var newFood = Instantiate(foodPrefab, foodParent);

            //var randomFoodObject = GameManager.instance.foodManager.GenerateRandomFood();

            var randomFoodObject = GameManager.instance.cookingRecipeManager.GenerateRandomIngredient();
            newFood.SetFood(randomFoodObject);

            SetFoodItem(newFood);
        }

        void SetFoodItem(CookingFoodItem newFood)
        {
            var emptySlot = GameManager.instance.boardManager.GetEmptySlot();

            if (emptySlot != null)
            {
                emptySlot.SetFoodItem(newFood);
                foodItems.Add(newFood);
            }
            else
            {
                Destroy(newFood.gameObject);
            }
        }

        bool CheckCollisions(CookingFoodItem target)
        {
            foreach(var item in foodItems)
            {
                if(CheckCollision(target, item))
                {
                    return true;
                }
            }

            return false;
        }

        bool CheckCollision(CookingFoodItem item1, CookingFoodItem item2)
        {
            float radius1 = item1.overlay.rectTransform.rect.width / 2f;
            float radius2 = item2.overlay.rectTransform.rect.width / 2f;

            var distance = Vector2.Distance(item1.transform.position, item2.transform.position);

            return distance <= radius1 + radius2;
        }

        public void OnClickFood(CookingFoodItem food)
        {
            GameManager.instance.cookingRecipeManager.AddFood(food);
            GenerateRandomRecipe();
            RemoveFood(food);
        }

        public void RemoveFood(CookingFoodItem food)
        {
            Destroy(food.gameObject);
            foodItems.Remove(food);
        }

        void GenerateRandomRecipe()
        {
            var randomMysteryRoll = UnityEngine.Random.Range(0, 1f);
            if(randomMysteryRoll > .1f)
            {
                //return;
            }

            if (foodItems.Count >= maxFoodCount)
            {
                return;
            }

            var randomRecipe = GameManager.instance.foodManager.GenerateRandomRecipe();

            var newFood = Instantiate(recipePrefab, foodParent) as CookingRecipeItem;
                                    
            newFood.SetMysteryFood(randomRecipe);

            SetFoodItem(newFood);

        }
    }
}
