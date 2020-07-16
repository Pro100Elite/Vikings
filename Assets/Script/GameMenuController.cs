using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{

    public GameObject endGameUi;
    public GameObject scoreInGame;
    public Text score;
    public SceneFader sceneFader;

    public string levelToLoad = "GameLevel";

    private ScoreController scoreController;
    private PlayerState player;

    private void Start()
    {
        scoreController = GetComponent<ScoreController>();
        player = FindObjectOfType<PlayerState>();
    }

    void Update()
    {       
        if (player.hp <= 0)
        {
            endGameUi.SetActive(true);
            scoreInGame.SetActive(false);
        }
        if (endGameUi.activeInHierarchy)
        {
            score.text = scoreController.value.ToString();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Restrart()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }  
}
