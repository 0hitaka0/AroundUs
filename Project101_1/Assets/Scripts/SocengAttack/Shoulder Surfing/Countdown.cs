using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float countdownDuration = 10f;
    public Text countdownText;
    public GameObject popUpText;

    public float countdownTimer;

    void Start()
    {
        countdownTimer = countdownDuration;
        UpdateCountdownText();
        popUpText.SetActive(false);
    }

    void Update()
    {
        if (countdownTimer > 0f)
        {
            countdownTimer -= Time.deltaTime;
            UpdateCountdownText();

            if (countdownTimer <= 0f)
            {
                ShowPopUpText();
            }
        }
    }

    void UpdateCountdownText()
    {
        int seconds = Mathf.CeilToInt(countdownTimer);
        countdownText.text = seconds.ToString();
    }

    void ShowPopUpText()
    {
        popUpText.SetActive(true);
    }
}
