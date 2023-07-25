using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShoulderSurfing : MonoBehaviour
{
    public Countdown countdown;
    public ProgressBar progressBar;

    public float catchThreshold = 0.9f; // Threshold for catching the player
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    private bool isGameOver;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        isGameOver = false;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            if (progressBar.progressBar.value >= catchThreshold)
            {
                GameOver("Caught!");
            }
            else if (countdown.countdownTimer <= 0f)
            {
                GameOver("Time's up!");
            }
        }
    }

    private void GameOver(string message)
    {
        isGameOver = true;
        countdown.enabled = false;
        progressBar.enabled = false;
        gameOverText.text = message;
        gameOverPanel.SetActive(true);
    }
}
