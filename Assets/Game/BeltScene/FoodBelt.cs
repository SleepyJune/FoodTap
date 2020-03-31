using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.BeltScene
{
    public class FoodBelt : MonoBehaviour
    {
        [NonSerialized]
        public RectTransform rectTransform;
        [NonSerialized]
        public float beltWidth;
        [NonSerialized]
        public FoodBelt prev;        

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            beltWidth = rectTransform.rect.width;

            SetPositionAfter(prev);
        }

        public void SetPositionAfter(FoodBelt prevBelt)
        {
            if(prevBelt != null)
            {
                transform.position = new Vector2(prevBelt.transform.position.x - prevBelt.beltWidth, transform.position.y);

            }
        }
    }
}
