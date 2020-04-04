using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Game.BeltScene;
using Assets.Game.CookingScene;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public GameDatabaseManager databaseManager;
    public FoodManager foodManager;

    public BeltManager beltManager;
    public BeltFoodManager beltFoodManager;
    public BeltRecipeManager beltRecipeManager;

    public CookingFoodManager cookingFoodManager;
    public BoardManager boardManager;
    public CharacterManager characterManager;
    public CookingRecipeManager cookingRecipeManager;
    public LevelManager levelManager;

    public Transform gameOverPopup;
    public Transform victoryPopup;
        
    [System.NonSerialized]
    public bool isGameOver = false;

    public static GameManager instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && this != _instance)
        {
            _instance.beltManager = beltManager;
            _instance.beltFoodManager = beltFoodManager;
            _instance.beltRecipeManager = beltRecipeManager;

            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void Initialize()
    {
        foodManager.Initialize();
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            gameOverPopup.gameObject.SetActive(true);
            isGameOver = true;
        }   
    }

    public void Victory()
    {
        if (!isGameOver)
        {
            victoryPopup.gameObject.SetActive(true);
            isGameOver = true;
        }
    }

    public void NextLevel()
    {
        victoryPopup.gameObject.SetActive(false);

        levelManager.NextLevel();
        characterManager.ResetHealth();
        cookingFoodManager.Restart();
        cookingRecipeManager.Restart();

        isGameOver = false;
    }

    public void RestartGame()
    {
        gameOverPopup.gameObject.SetActive(false);
        
        levelManager.Restart();
        characterManager.ResetHealth();
        cookingFoodManager.Restart();
        cookingRecipeManager.Restart();

        isGameOver = false;
    }
}
