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
        public Transform foodParent;

        [NonSerialized]
        public List<CookingFoodItem> foodItems = new List<CookingFoodItem>();

        public float spawnSpeed = 200;

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

            /*
             * if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                
                if (hit)
                {
                    var foodItem = hit.collider.gameObject.GetComponent<CookingFoodItem>();

                    Debug.Log(hit);

                    if (foodItem != null)
                    {
                        RemoveFood(foodItem);
                    }
                }
            }
             * */
        }

        void SpawnFoods()
        {
            if (Time.time - lastSpawnTimer > spawnSpeed / 1000f)
            {
                CreateNewFood();

                lastSpawnTimer = Time.time;
            }
        }

        void CreateNewFood()
        {
            if(foodItems.Count >= maxFoodCount)
            {
                return;
            }

            var newFood = Instantiate(foodPrefab, foodParent);

            var randomFoodObject = GameManager.instance.foodManager.GenerateRandomFood();
            newFood.SetFood(randomFoodObject);


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

        public void RemoveFood(CookingFoodItem food)
        {
            Destroy(food.gameObject);
            foodItems.Remove(food);
        }
    }
}
