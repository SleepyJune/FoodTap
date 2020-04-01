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
    public class CookingFoodItem : MonoBehaviour, IPointerDownHandler
    {
        [NonSerialized]
        public FoodObject food;
        public Image image;
        public Image overlay;
        public Image bar;

        [NonSerialized]
        public float percent = 0f;

        float cookingSpeed = 2;

        void Start()
        {
            overlay.alphaHitTestMinimumThreshold = .05f;
        }

        void Update()
        {
            percent += cookingSpeed/100f * Time.deltaTime;

            UpdateBar();
        }

        void UpdateBar()
        {
            bar.fillAmount = percent;
        }

        public void SetFood(FoodObject food)
        {
            cookingSpeed = UnityEngine.Random.Range(10, 50);

            this.food = food;

            if (food != null)
            {
                image.sprite = food.icon;
            }
        }        

        public void OnPointerDown(PointerEventData eventData)
        {
            if(percent >= 1f)
            {
                GameManager.instance.cookingFoodManager.RemoveFood(this);
            }
        }
    }
}
