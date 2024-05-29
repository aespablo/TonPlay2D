﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    public GameObject playButton;
    public GameObject mainMenuUI;
    public GameObject gameMenuUI;
    public TextMeshProUGUI score;

    public GameObject shopMenu;
    public GameObject[] shopMenuItems;
    public TextMeshProUGUI shopMenuAvailablePoints;
    public CircleCollider2D playButtonCollider;
    public GameObject pauseMenu;
    public GameObject transitionImage;
    public AudioSource buttonSound;
    public GameObject soundOff;

    public GameObject topMenu;
    public GameObject replyButton;

    private void Start()
    {
        Application.targetFrameRate = 300;
    }

    public void StartTheGame()
    {
        Vars.StartGame = true;
        Vars.CurrentMenu = 1;
        transitionImage.GetComponent<MenuTransition>().enabled = true;
        buttonSound.Play();
    }

    public void ShowGamePlayMenu()
    {
        GameObject game = Instantiate(Resources.Load("Game", typeof(GameObject))) as GameObject;
        game!.name = "Game";
        Destroy(GameObject.Find("MainMenu"));
        playButton.SetActive(false);
        mainMenuUI.SetActive(false);
        gameMenuUI.SetActive(true);
        score.text = "POINTS: 0";
    }

    public void BackToTheMainMenu()
    {
        Time.timeScale = 1;
        Vars.StartGame = false;
        Vars.CurrentMenu = 0;
        transitionImage.GetComponent<MenuTransition>().enabled = true;
        Destroy(GameObject.Find("player"));
        buttonSound.Play();
    }

    public void ShowMainMenu()
    {
        Destroy(GameObject.Find("Game"));
        GameObject game = Instantiate(Resources.Load("MainMenu", typeof(GameObject))) as GameObject;
        game.name = "MainMenu";
        playButton.SetActive(true);
        mainMenuUI.SetActive(true);
        gameMenuUI.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void ShowShopMenu()
    {
        shopMenu.transform.localScale = new Vector2(1, 1);
        playButtonCollider.enabled = false;
        shopMenuAvailablePoints.text =
            "POINTS: " + (PlayerPrefs.GetInt("totalPoints") - PlayerPrefs.GetInt("spentPoints"));
        buttonSound.Play();
    }

    public void HideShopMenu()
    {
        shopMenu.transform.localScale = new Vector2(0, 1);
        playButtonCollider.enabled = true;
        buttonSound.Play();
    }

    public void UnSelectAllShopItems()
    {
        for (int i = 0; i < shopMenuItems.Length; i++)
        {
            shopMenuItems[i].GetComponent<Image>().color = new Color(0.1698113f, 0.1698113f, 0.1698113f, 1f);
        }
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        buttonSound.Play();
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        buttonSound.Play();
    }

    public void Reply()
    {
        buttonSound.Play();
        Time.timeScale = 1;
        Vars.CurrentMenu = 2;
        transitionImage.GetComponent<MenuTransition>().enabled = true;
    }

    public void GameReply()
    {
        Vars.Reset();
        replyButton.transform.localScale = new Vector2(0, 4);
        replyButton.GetComponent<CircleCollider2D>().enabled = false;
        replyButton.GetComponent<SpriteRenderer>().enabled = false;
        pauseMenu.SetActive(false);
        topMenu.SetActive(true);
        Destroy(GameObject.Find("Game"));
        GameObject.Find("GameOverMenu").transform.localScale = new Vector2(0, 1);
        GameObject game = Instantiate(Resources.Load("Game", typeof(GameObject))) as GameObject;
        game!.name = "Game";
        score.text = "POINTS: 0";
    }

    public void SoundOnOff()
    {
        buttonSound.Play();
        if (Mathf.Approximately(AudioListener.volume, 1))
        {
            AudioListener.volume = 0;
            soundOff.SetActive(true);
        }
        else
        {
            AudioListener.volume = 1;
            soundOff.SetActive(false);
        }
    }

    public void Quit()
    {
        buttonSound.Play();
        Application.Quit();
    }
}