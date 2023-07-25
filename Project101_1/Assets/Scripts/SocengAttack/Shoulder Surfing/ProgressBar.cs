using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public Transform player;
    public Transform npc;
    public float detectionRange;
    public float catchDistance; // Distance at which the player gets caught
    public Slider progressBar;
    public float increaseSpeed = 1f; // Speed at which the progress bar increases
    public float decreaseSpeed = 0.5f; // Speed at which the progress bar decreases
    public float resetTime = 3f; // Time until the progress bar resets

    public GameObject popUpText;
    public TextMeshProUGUI popUpTextMeshPro;
    public GameObject infoPanel;

    private float progressBarValue = 0f;
    private float timer = 0f;
    private bool isPlayerCaught = false;

    private void Update()
    {
        // Check if the player is within the detection range of the NPC
        float distance = Vector3.Distance(player.position, npc.position);
        bool isPlayerClose = distance <= detectionRange;

        if (isPlayerCaught)
        {
            // Player is already caught, show game over pop-up or take appropriate action
            // Reset the progress bar after a certain time
            timer += Time.deltaTime;
            if (timer >= resetTime)
            {
                ResetProgressBar();
            }
            return;
        }

        // Update the progress bar value based on player's proximity to the NPC
        if (isPlayerClose)
        {
            // Player is behind the NPC, increase progress
            progressBarValue += increaseSpeed * Time.deltaTime;
            progressBarValue = Mathf.Clamp01(progressBarValue);
        }
        else
        {
            // Player is not close, decrease progress
            progressBarValue -= decreaseSpeed * Time.deltaTime;
            progressBarValue = Mathf.Clamp01(progressBarValue);
        }

        // Update the progress bar value
        progressBar.value = progressBarValue;

        if (progressBarValue >= 1f)
        {
            // Player is caught
            isPlayerCaught = true;
            ShowPopUpText("Kamu ketahuan oleh NPC");
            infoPanel.SetActive(true);
        }
    }

    private void ShowPopUpText(string text)
    {
        popUpTextMeshPro.text = text;
        popUpText.SetActive(true);
    }

    private void ResetProgressBar()
    {
        progressBarValue = 0f;
        timer = 0f;
        isPlayerCaught = false;
        popUpText.SetActive(false);
        infoPanel.SetActive(false);
    }
}
