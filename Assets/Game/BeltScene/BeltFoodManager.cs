using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.BeltScene
{
    public class BeltFoodManager : MonoBehaviour
    {
        public FoodBeltItem foodPrefab;
        public Transform foodParent;

        [NonSerialized]
        public List<FoodBeltItem> foodItems = new List<FoodBeltItem>();

        public float spawnSpeed = 200;

        [NonSerialized]
        public float lastSpawnTimer = 0;

        void Start()
        {
            foodParent.DeleteChildren();
        }

        void Update()
        {
            SpawnFoods();
            MoveFoods();
            UpdateFoods();
        }

        void SpawnFoods()
        {
            if (Time.time - lastSpawnTimer > spawnSpeed / 1000f)
            {
                CreateNewFood();

                lastSpawnTimer = Time.time;
            }
        }

        void MoveFoods()
        {
            var speed = GameManager.instance.beltManager.GetBeltSpeed();

            foreach (var food in foodItems)
            {
                food.transform.position = new Vector2(food.transform.position.x + speed * Time.deltaTime, food.transform.position.y);
            }
        }

        void UpdateFoods()
        {
            List<FoodBeltItem> removeList = new List<FoodBeltItem>();

            foreach (var food in foodItems)
            {
                if (food.transform.position.x > 1500)
                {
                    removeList.Add(food);
                }
            }

            for(int i = 0; i < removeList.Count; i++)
            {
                var food = removeList[i];
                RemoveFood(food);
            }

        }

        public void CreateNewFood()
        {
            var newFood = Instantiate(foodPrefab, foodParent);

            var randomFoodObject = GameManager.instance.foodManager.GenerateRandomFood();
            newFood.SetFood(randomFoodObject);

            var randomHeight = UnityEngine.Random.Range(-200, 200);

            newFood.transform.localPosition = new Vector2(-800, randomHeight);

            foodItems.Add(newFood);
        }

        public void AddFoodFromBelt(FoodBeltItem food)
        {
            if (!GameManager.instance.beltRecipeManager.AddFoodFromBelt(food))
            {
                GameManager.instance.beltManager.speed += 50;
            }

            RemoveFood(food);
        }
                
        public void RemoveFood(FoodBeltItem food)
        {
            Destroy(food.gameObject);
            foodItems.Remove(food);
        }
    }
}
