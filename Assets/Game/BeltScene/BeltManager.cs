using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Assets.Game.BeltScene
{
    public class BeltManager : MonoBehaviour
    {
        public FoodBelt beltPrefab;
        public Transform beltParent;

        public float speed = 500;
        public float speedMultiplier = 1;

        List<FoodBelt> belts = new List<FoodBelt>();

        int maxBelts = 3;
        void Start()
        {
            beltParent.DeleteChildren();

            for (int i = 0; i < maxBelts; i++)
            {
                CreateNewBelt();
            }
        }

        public void CreateNewBelt()
        {
            var newBelt = Instantiate(beltPrefab, beltParent);

            if (belts.Count > 0)
            {
                var lastBelt = belts[belts.Count - 1];
                newBelt.prev = lastBelt;
            }

            belts.Add(newBelt);
        }

        void Update()
        {
            MoveBelt();
            UpdateBelt();
        }

        public float GetBeltSpeed()
        {
            return speed * speedMultiplier;
        }

        void MoveBelt()
        {
            var finalSpeed = GetBeltSpeed();

            foreach(var belt in belts)
            {
                belt.transform.position = new Vector2(belt.transform.position.x + finalSpeed * Time.deltaTime, belt.transform.position.y);
            }       
        }

        void UpdateBelt()
        {
            if (belts.Count > 0 && belts[0].transform.position.x > 1500)
            {
                Destroy(belts[0].gameObject);
                belts.RemoveAt(0);

                CreateNewBelt();
            }
        }
    }
}
