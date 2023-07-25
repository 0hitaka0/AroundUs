using System.Collections;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public TextMeshProUGUI coinDisplay;
    public GameObject coinEarnPopup;
    public TextMeshProUGUI coinEarnText;
    public GameObject continueText;
    public GameObject badgePopup;
    public Inventory playerInventory; // Reference to the Inventory scriptable object

    private static CoinTextManager instance;
    private int coins; // Keep track of the coins count internally

    private void Start()
    {
        instance = this;
        coins = playerInventory.coins; // Initialize coins count from the player's inventory
        UpdateCoinCount();
    }

    public static CoinTextManager GetInstance()
    {
        return instance;
    }

    public void ShowCoinEarnPopup(int coinsEarned)
    {
        // Update the coins count
        coins += coinsEarned;

        // Update the coin display text
        UpdateCoinCount();

        // Show the coin earn popup
        coinEarnPopup.SetActive(true);
        StartCoroutine(BlinkContinueText());
    }

    private void UpdateCoinCount()
    {
        coinDisplay.text = coins.ToString();
    }

    private IEnumerator BlinkContinueText()
    {
        while (true)
        {
            continueText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            continueText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ContinueButtonClicked()
    {
        badgePopup.SetActive(true);
    }
}
