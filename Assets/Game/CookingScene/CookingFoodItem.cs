using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

namespace Assets.Game.CookingScene
{
    public class CookingFoodItem : MonoBehaviour
    {
        [NonSerialized]
        public FoodObject food;
        [NonSerialized]
        public FoodRecipeObject recipe;

        public Image image;
        public Image overlay;
        public Image bar;
        public Text countText;
        public Animator anim;

        [NonSerialized]
        public float percent = 0f;
        [NonSerialized]
        int count = 5;

        [NonSerialized]
        public float cookingSpeed = 2;

        [NonSerialized]
        public bool isRecipe = false;

        void Start()
        {
            overlay.alphaHitTestMinimumThreshold = .05f;
        }

        void Update()
        {            
            percent += cookingSpeed / 100f * Time.deltaTime;

            UpdateBar();
        }

        void UpdateBar()
        {
            bar.fillAmount = percent;

            var hue = Mathf.Lerp(120, 0, percent);

            bar.color = Color.HSVToRGB(hue / 360f, 1, 1);

            if(percent >= 1f)
            {
                GameManager.instance.characterManager.DecreaseHealth();
                GameManager.instance.cookingFoodManager.RemoveFood(this);
            }
        }

        public virtual void SetFood(FoodObject food)
        {
            count = food.cookingTime;// UnityEngine.Random.Range(1, 10);

            var multiplier = GameManager.instance.cookingFoodManager.cookingSpeedMultiplier;
            cookingSpeed = multiplier * 100f / (count * 3);
            
            this.food = food;

            if (food != null)
            {
                image.sprite = food.icon;
            }
        }        

        public virtual void OnButtonPressed()
        {
            //DecreaseCount();

            if(percent >= .75f)
            {
                GameManager.instance.cookingFoodManager.OnClickFood(this);
            }
            else
            {
                anim.SetTrigger("shake");
            }
        }

        public virtual void DecreaseCount()
        {
            if (count <= 0)
            {
                GameManager.instance.cookingFoodManager.RemoveFood(this);
            }
        }
    }
}
