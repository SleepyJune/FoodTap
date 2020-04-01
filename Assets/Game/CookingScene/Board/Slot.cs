using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.CookingScene
{   
    public class Slot : MonoBehaviour
    {
        [NonSerialized]
        public CookingFoodItem foodItem;

        [NonSerialized]
        public Hex hexPosition;
        [NonSerialized]
        public Vector3 position;

        public HashSet<Slot> neighbours = new HashSet<Slot>();

        float slotSize;

        public void Initialize(Hex position, float slotSize)
        {
            this.hexPosition = position;
            this.position = position.ConvertCube();
                        
            this.slotSize = slotSize;

            RectTransform rect = GetComponent<RectTransform>();

            rect.sizeDelta = new Vector2(slotSize * 2f, slotSize * 2f);

            var worldPos = hexPosition.GetWorldPos(slotSize);
            transform.localPosition = worldPos;
        }

        public void SetFoodItem(CookingFoodItem foodItem)
        {
            this.foodItem = foodItem;

            foodItem.transform.SetParent(transform, false);
        }

        public void AddNeighbour(Slot neighbour)
        {
            neighbours.Add(neighbour);
        }

        public bool isNeighbour(Slot slot)
        {
            if (neighbours.Contains(slot))
            {
                return true;
            }

            return false;
        }

        public static bool operator ==(Slot value1, Slot value2)
        {
            if (object.ReferenceEquals(value1, null))
            {
                return object.ReferenceEquals(value2, null);
            }

            if (object.ReferenceEquals(value2, null))
            {
                return object.ReferenceEquals(value1, null);
            }

            return value1.position == value2.position;
        }

        public static bool operator !=(Slot value1, Slot value2)
        {
            return !(value1 == value2);
        }

        public override bool Equals(object obj)
        {
            return (obj is Slot) ? this == (Slot)obj : false;
        }

        public bool Equals(Slot other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return position.GetHashCode();
        }
    }
}
