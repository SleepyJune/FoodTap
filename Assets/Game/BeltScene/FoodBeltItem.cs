using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

namespace Assets.Game.BeltScene
{
    public class FoodBeltItem : MonoBehaviour, IPointerDownHandler
    {
        [NonSerialized]
        public FoodObject food;
        public Image image;

        public void SetFood(FoodObject food)
        {
            this.food = food;

            if (food != null)
            {
                image.sprite = food.icon;
            }
        }

        public void OnButtonClick()
        {
            GameManager.instance.beltFoodManager.RemoveFood(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {

            GameManager.instance.beltFoodManager.AddFoodFromBelt(this);
        }
    }
}
