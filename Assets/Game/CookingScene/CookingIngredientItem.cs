using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.CookingScene
{
    public class CookingIngredientItem : MonoBehaviour
    {
        public Image foodIcon;

        public Text countText;

        public Animator anim;

        [NonSerialized]
        public int count = 0;

        [NonSerialized]
        public FoodObject ingredient;

        public void SetIngredient(FoodObject ingredient, int count)
        {
            this.ingredient = ingredient;
            foodIcon.sprite = ingredient.icon;

            //count = UnityEngine.Random.Range(1, 10);
            this.count = count;

            countText.text = count.ToString();
        }

        public void DecreaseCount()
        {
            count = Math.Max(0, count - 1);
            countText.text = count.ToString();

            anim.SetTrigger("addFood");
        }
    }
}
