using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public bool gameStartedFromMainMenu, gameRestartedAfterPlayerDied;

    [HideInInspector]
    public int score, coinScore, lifeScore;

    void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {
        InitializeVariables();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += LevelFinishedLoading;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelFinishedLoading;
    }

    //void OnLevelWasLoaded()
    //{
    //    if(SceneManager.GetActiveScene().name == "Gameplay")
    //    {
    //        if (gameRestartedAfterPlayerDied)
    //        {
    //            GameplayController.instance.SetScore(score);
    //            GameplayController.instance.SetCoinScore(coinScore);
    //            GameplayController.instance.SetLifeScore(lifeScore);

    //            PlayerScore.scoreCount = score;
    //            PlayerScore.coinCount = coinScore;
    //            PlayerScore.lifeCount = lifeScore;
    //        }
    //        else if (gameStartedFromMainMenu)
    //        {
    //            PlayerScore.scoreCount = 0;
    //            PlayerScore.coinCount = 0;
    //            PlayerScore.lifeCount = 2;

    //            GameplayController.instance.SetScore(0);
    //            GameplayController.instance.SetCoinScore(0);
    //            GameplayController.instance.SetLifeScore(2);
    //        }
    //    }
    //}

    void LevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay")
        {
            if (gameRestartedAfterPlayerDied)
            {
                GameplayController.instance.SetScore(score);
                GameplayController.instance.SetCoinScore(coinScore);
                GameplayController.instance.SetLifeScore(lifeScore);

                PlayerScore.scoreCount = score;
                PlayerScore.coinCount = coinScore;
                PlayerScore.lifeCount = lifeScore;
            }
            else if (gameStartedFromMainMenu)
            {
                PlayerScore.scoreCount = 0;
                PlayerScore.coinCount = 0;
                PlayerScore.lifeCount = 2;

                GameplayController.instance.SetScore(0);
                GameplayController.instance.SetCoinScore(0);
                GameplayController.instance.SetLifeScore(2);
            }

        }
    }

    void InitializeVariables()
    {
        if(!PlayerPrefs.HasKey("Game Initialized"))
        {
            GamePreferenceses.SetEasyDifficulty(0);
            GamePreferenceses.SetEasyDifficultyCoinScore(0);
            GamePreferenceses.SetEasyDifficultyHighScore(0);

            GamePreferenceses.SetMediumDifficulty(1);
            GamePreferenceses.SetMediumDifficultyCoinScore(0);
            GamePreferenceses.SetMediumDifficultyHighScore(0);

            GamePreferenceses.SetHardDifficulty(0);
            GamePreferenceses.SetHardDifficultyCoinScore(0);
            GamePreferenceses.SetHardDifficultyHighScore(0);

            GamePreferenceses.SetMusicState(0);

            PlayerPrefs.SetInt("Game Initialized", 123);
        }
    }

    void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CheckGameStatus(int score, int coinScore, int lifeScore)
    {
        if(lifeScore < 0)
        {
            if(GamePreferenceses.GetEasyDifficulty() == 1)
            {
                int highScore = GamePreferenceses.GetEasyDifficultyHighScore();
                int coinHighScore = GamePreferenceses.GetEasyDifficultyCoinScore();

                if (highScore < score)
                    GamePreferenceses.SetEasyDifficultyHighScore(score);
                if (coinHighScore < coinScore)
                    GamePreferenceses.SetEasyDifficultyCoinScore(coinScore);
            }
            if (GamePreferenceses.GetMediumDifficulty() == 1)
            {
                int highScore = GamePreferenceses.GetMediumDifficultyHighScore();
                int coinHighScore = GamePreferenceses.GetMediumDifficultyCoinScore();

                if (highScore < score)
                    GamePreferenceses.SetMediumDifficultyHighScore(score);
                if (coinHighScore < coinScore)
                    GamePreferenceses.SetMediumDifficultyCoinScore(coinScore);
            }
            if (GamePreferenceses.GetHardDifficulty() == 1)
            {
                int highScore = GamePreferenceses.GetHardDifficultyHighScore();
                int coinHighScore = GamePreferenceses.GetHardDifficultyCoinScore();

                if (highScore < score)
                    GamePreferenceses.SetHardDifficultyHighScore(score);
                if (coinHighScore < coinScore)
                    GamePreferenceses.SetHardDifficultyCoinScore(coinScore);
            }


            gameStartedFromMainMenu = false;
            gameRestartedAfterPlayerDied = false;

            AdsControll.instance.ShowInterstitial();

            GameplayController.instance.GameOverShowPanel(score, coinScore);
        }
        else
        {
            this.score = score;
            this.coinScore = coinScore;
            this.lifeScore = lifeScore;

            //GameplayController.instance.SetScore(score);
            //GameplayController.instance.SetCoinScore(coinScore);
            //GameplayController.instance.SetLifeScore(lifeScore);

            gameStartedFromMainMenu = false;
            gameRestartedAfterPlayerDied = true;

            GameplayController.instance.PlayerDiedRestartTheGame();
        }
    }
}
