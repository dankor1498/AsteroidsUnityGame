using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject scoreText, scoreTextEnemy, startButton, menu;

    private int score = 0;
    private int scoreEnemy = 0;

    private bool isStarted = false;

    public bool isGameStarted()
    {
        return isStarted;
    }

    private void Start()
    {
        startButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
            isStarted = true;
            menu.SetActive(false);
        });
    }

    public void increaseScore(int increment)
    {
        score += increment;
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Астероїди: " + score;
    }

    public void increaseEnemy(int increment)
    {
        scoreEnemy += increment;
        scoreTextEnemy.GetComponent<UnityEngine.UI.Text>().text = "Кораблі: " + scoreEnemy;
    }
}
