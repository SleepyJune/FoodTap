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
}
