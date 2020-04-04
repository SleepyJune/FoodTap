using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.CookingScene
{
    public class CharacterManager : MonoBehaviour
    {
        [NonSerialized]
        public int health = 3;

        public GameObject healthPrefab;
        public Transform healthParent;

        Stack<GameObject> healthSlots = new Stack<GameObject>();

        void Start()
        {
            ResetHealth();
        }

        public void ResetHealth()
        {
            health = 3;
            healthParent.DeleteChildren();

            for (int i = 0; i < health; i++)
            {
                var newHealthSlot = Instantiate(healthPrefab, healthParent);

                healthSlots.Push(newHealthSlot);
            }
        }

        public void DecreaseHealth()
        {
            if (GameManager.instance.isGameOver)
            {
                return;
            }

            health -= 1;

            if(healthSlots.Count > 0)
            {
                var healthSlot = healthSlots.Pop();
                Destroy(healthSlot.gameObject);
            }
            
            if(health <= 0)
            {
                GameManager.instance.GameOver();
            }
        }
    }
}
