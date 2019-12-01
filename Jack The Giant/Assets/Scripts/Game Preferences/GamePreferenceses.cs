using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePreferenceses
{
    public static string EasyDifficulty = "EasyDifficulty";
    public static string MediumDifficulty = "MediumDifficulty";
    public static string HardDifficulty = "HardDifficulty";

    public static string EasyDifficultyHighScore = "EasyDifficultyHighScore";
    public static string MediumDifficultyHighScore = "MediumDifficultyHighScore";
    public static string HardDifficultyHighScore = "HardDifficultyHighScore";

    public static string EasyDifficultyCoinScore = "EasyDifficultyCoinScore";
    public static string MediumDifficultyCoinScore = "MediumDifficultyCoinScore";
    public static string HardDifficultyCoinScore = "HardDifficultyCoinScore";

    public static string IsMusicOn = "IsMusicOn";

    // NOTE we are going to use integers to represent boolean variables
    // 0 - false , 1 - true

    public static int GetMusicState()
    {
        return PlayerPrefs.GetInt(GamePreferenceses.IsMusicOn);
    }

    public static void SetMusicState(int state)
    {
        PlayerPrefs.SetInt(GamePreferenceses.IsMusicOn, state);
    }

    public static int GetEasyDifficulty()
    {
        return PlayerPrefs.GetInt(EasyDifficulty);
    }

    public static void SetEasyDifficulty(int state)
    {
        PlayerPrefs.SetInt(GamePreferenceses.EasyDifficulty, state);
    }

    public static int GetMediumDifficulty()
    {
        return PlayerPrefs.GetInt(MediumDifficulty);
    }

    public static void SetMediumDifficulty(int state)
    {
        PlayerPrefs.SetInt(GamePreferenceses.MediumDifficulty, state);
    }

    public static int GetHardDifficulty()
    {
        return PlayerPrefs.GetInt(HardDifficulty);
    }

    public static void SetHardDifficulty(int state)
    {
        PlayerPrefs.SetInt(GamePreferenceses.HardDifficulty, state);
    }


    public static int GetEasyDifficultyHighScore()
    {
        return PlayerPrefs.GetInt(EasyDifficultyHighScore);
    }

    public static void SetEasyDifficultyHighScore(int state)
    {
        PlayerPrefs.SetInt(GamePreferenceses.EasyDifficultyHighScore, state);
    }

    public static int GetMediumDifficultyHighScore()
    {
        return PlayerPrefs.GetInt(MediumDifficultyHighScore);
    }

    public static void SetMediumDifficultyHighScore(int state)
    {
        PlayerPrefs.SetInt(GamePreferenceses.MediumDifficultyHighScore, state);
    }

    public static int GetHardDifficultyHighScore()
    {
        return PlayerPrefs.GetInt(HardDifficultyHighScore);
    }

    public static void SetHardDifficultyHighScore(int state)
    {
        PlayerPrefs.SetInt(GamePreferenceses.HardDifficultyHighScore, state);
    }


    public static int GetEasyDifficultyCoinScore()
    {
        return PlayerPrefs.GetInt(EasyDifficultyCoinScore);
    }

    public static void SetEasyDifficultyCoinScore(int state)
    {
        PlayerPrefs.SetInt(GamePreferenceses.EasyDifficultyCoinScore, state);
    }

    public static int GetMediumDifficultyCoinScore()
    {
        return PlayerPrefs.GetInt(MediumDifficultyCoinScore);
    }

    public static void SetMediumDifficultyCoinScore(int state)
    {
        PlayerPrefs.SetInt(GamePreferenceses.MediumDifficultyCoinScore, state);
    }

    public static int GetHardDifficultyCoinScore()
    {
        return PlayerPrefs.GetInt(HardDifficultyCoinScore);
    }

    public static void SetHardDifficultyCoinScore(int state)
    {
        PlayerPrefs.SetInt(GamePreferenceses.HardDifficultyCoinScore, state);
    }
}
