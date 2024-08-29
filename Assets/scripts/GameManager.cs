using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public Button restartButton;
    public GameObject player;
    public GameObject winText;
    public GameObject loseText;
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        Time.timeScale = 1;

        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -10)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        panel.SetActive(true);
        loseText.SetActive(true);
        winText.SetActive(false);
        Time.timeScale = 0;
    }


    public void Win()
    {
        panel.SetActive(true);
        winText.SetActive(true);
        loseText.SetActive(false);
        Time.timeScale = 0;
    }
    public void RestartGame()
    {
        panel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
