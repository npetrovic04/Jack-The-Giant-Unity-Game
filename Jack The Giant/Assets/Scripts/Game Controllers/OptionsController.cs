using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    private GameObject easySign, mediumSign, hardSign;

	// Use this for initialization
	void Start ()
    {
        SetTheDifficulty();
	}

    void SetInitialDifficulty(string difficulty)
    {
        switch (difficulty)
        { 
        case "easy":
            mediumSign.SetActive(false);
            hardSign.SetActive(false);
            break;

        case "medium":
            easySign.SetActive(false);
            hardSign.SetActive(false);
            break;

        case "hard":
            easySign.SetActive(false);
            mediumSign.SetActive(false);
            break;
        }
    }

    void SetTheDifficulty()
    {
        if(GamePreferenceses.GetEasyDifficulty() == 1)
        {
            SetInitialDifficulty("easy");
        }
        if (GamePreferenceses.GetMediumDifficulty() == 1)
        {
            SetInitialDifficulty("medium");
        }
        if (GamePreferenceses.GetHardDifficulty() == 1)
        {
            SetInitialDifficulty("hard");
        }
    }

    public void EasyDifficulty()
    {
        GamePreferenceses.SetEasyDifficulty(1);
        GamePreferenceses.SetMediumDifficulty(0);
        GamePreferenceses.SetHardDifficulty(0);

        easySign.SetActive(true);
        mediumSign.SetActive(false);
        hardSign.SetActive(false);
    }

    public void MediumDifficulty()
    {
        GamePreferenceses.SetEasyDifficulty(0);
        GamePreferenceses.SetMediumDifficulty(1);
        GamePreferenceses.SetHardDifficulty(0);

        easySign.SetActive(false);
        mediumSign.SetActive(true);
        hardSign.SetActive(false);
    }

    public void HardDifficulty()
    {
        GamePreferenceses.SetEasyDifficulty(0);
        GamePreferenceses.SetMediumDifficulty(0);
        GamePreferenceses.SetHardDifficulty(1);

        easySign.SetActive(false);
        mediumSign.SetActive(false);
        hardSign.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
