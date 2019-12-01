using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControllers : MonoBehaviour
{
    [SerializeField]
    private Button musicBtn;

    [SerializeField]
    private Sprite[] musicIcons;

	// Use this for initialization
	void Start ()
    {
        CheckToPlayMusic();

    }

    void CheckToPlayMusic()
    {
        if(GamePreferenceses.GetMusicState() == 1)
        {
            MusicController.instance.PlayMusic(true);
            musicBtn.image.sprite = musicIcons[1];
        }
        else
        {
            MusicController.instance.PlayMusic(false);
            musicBtn.image.sprite = musicIcons[0];
        }
    }
	
	public void StartGame()
    {
        GameManager.instance.gameStartedFromMainMenu = true;
        //SceneManager.LoadScene("Gameplay");
        SceneFader.instance.LoadLevel("Gameplay");
    }

    public void HighscoreMenu()
    {
        SceneManager.LoadScene("HighscoreScene");
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void MusicButton()
    {
        if(GamePreferenceses.GetMusicState() == 0)
        {
            GamePreferenceses.SetMusicState(1);
            MusicController.instance.PlayMusic(true);
            musicBtn.image.sprite = musicIcons[1];
        }
        else if(GamePreferenceses.GetMusicState() == 1)
        {
            GamePreferenceses.SetMusicState(0);
            MusicController.instance.PlayMusic(false);
            musicBtn.image.sprite = musicIcons[0];
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
