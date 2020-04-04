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
    public class CookingRecipeItem : CookingFoodItem
    {
        [NonSerialized]
        int count = 5;

        [NonSerialized]
        bool isActivated = false;

        void Start()
        {
            overlay.alphaHitTestMinimumThreshold = .05f;
        }

        void Update()
        {
            if (!isActivated)
            {
                percent += cookingSpeed / 100f * Time.deltaTime;

                UpdateBar();
            }
        }

        void UpdateBar()
        {
            bar.fillAmount = percent;

            var hue = Mathf.Lerp(120, 0, percent);

            bar.color = Color.HSVToRGB(hue / 360f, 1, 1);

            if (percent >= 1f)
            {
                GameManager.instance.cookingFoodManager.RemoveFood(this);
            }
        }

        public void SetMysteryFood(FoodRecipeObject recipe)
        {
            count = 1;
            cookingSpeed = 100f / (count * 3);

            food = recipe.dish;

            isRecipe = true;

            if (food != null)
            {
                image.sprite = food.icon;
            }
        }

        public override void OnButtonPressed()
        {
            if (isActivated)
            {
                //do cool stuff
                //GameManager.instance.cookingFoodManager.RemoveFood(this);
            }
            else
            {
                if (percent >= .75f)
                {
                    anim.SetTrigger("rainbow");

                    isActivated = true;

                    percent = 1f;
                    bar.fillAmount = 1f;
                }
                else
                {
                    anim.SetTrigger("shake");
                }
            }
        }

        public override void DecreaseCount()
        {            

        }
    }
}
