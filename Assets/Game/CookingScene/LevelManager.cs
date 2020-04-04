using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.CookingScene
{
    public class LevelManager : MonoBehaviour
    {
        public Text levelText;

        //[NonSerialized]
        public int currentLevel = 1;

        void Start()
        {
            currentLevel -= 1;
            NextLevel();
        }

        public void Restart()
        {
            currentLevel = 0;
            NextLevel();
        }

        public void UpdateLevelText()
        {
            levelText.text = "Level " + currentLevel.ToString();
        }

        public void NextLevel()
        {
            currentLevel += 1;
            UpdateLevelText();

            //GameManager.instance.RestartGame();
            GameManager.instance.cookingFoodManager.spawnSpeed = Math.Max(500, 2000 - 125 * currentLevel);
            GameManager.instance.cookingFoodManager.cookingSpeedMultiplier = 1 + .05f * currentLevel;
            GameManager.instance.cookingRecipeManager.maxIngredientCount = (int)Math.Max(1, Math.Round(1 * currentLevel / 2f));
        }
    }
}
