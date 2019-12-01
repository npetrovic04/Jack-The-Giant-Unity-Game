using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour
{
    [SerializeField]
    private Text scoreText, coinText;

	void Start ()
    {
        SetScoreBasedOnDifficulty();
	}

    void SetScore(int score, int coinScore)
    {
        scoreText.text = score.ToString();
        coinText.text = coinScore.ToString();
    }

    void SetScoreBasedOnDifficulty()
    {
        if(GamePreferenceses.GetEasyDifficultyCoinScore() == 1)
        {
            SetScore(GamePreferenceses.GetEasyDifficultyHighScore(), GamePreferenceses.GetEasyDifficultyCoinScore());
        }
        if (GamePreferenceses.GetMediumDifficulty() == 1)
        {
            SetScore(GamePreferenceses.GetMediumDifficultyHighScore(), GamePreferenceses.GetMediumDifficultyCoinScore());
        }
        if (GamePreferenceses.GetHardDifficulty() == 1)
        {
            SetScore(GamePreferenceses.GetEasyDifficultyHighScore(), GamePreferenceses.GetHardDifficultyCoinScore());
        }
    }
	
	public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
